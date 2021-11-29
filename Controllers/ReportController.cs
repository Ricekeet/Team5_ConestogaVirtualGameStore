using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class ReportController : Controller
    {
        private readonly string memberDetailsQuery = @"
                    SELECT u.UserName, u.Email, u.FirstName, u.LastName, u.PhoneNumber, u.gender, FORMAT(u.birthday, 'MM/dd/yy')
					FROM AspNetUsers u 
                    JOIN AspNetUserRoles ur ON u.Id = ur.UserId 
                    JOIN AspNetRoles r ON ur.RoleId = r.Id 
                    WHERE r.NormalizedName = 'MEMBER'";
        private readonly string gameDetailsQuery = @"
                    SELECT g.Name, FORMAT(g.releaseDate, 'MM/dd/yy'), g.Price, g.Inventory, ge.name, p.name, g.Description
					FROM Game g 
                    JOIN Genre ge ON g.genreId = ge.genreID
                    JOIN Platform p on g.platformID= p.platformID;";
        private readonly string popularGamesQuery = @"
                    SELECT g.Name, ge.name, FORMAT(g.releaseDate, 'MM/dd/yy'), g.Price, p.name,  SUM(oi.gameID), '$' + CAST(g.price * SUM(oi.gameID) AS VARCHAR(15)) 
					FROM Game g 
					JOIN Platform p ON p.platformID = g.platformID
					JOIN Genre ge ON ge.genreID = g.genreID
                    JOIN OrderItem oi ON g.gameID = oi.gameID
					group by g.name, g.Price, g.Inventory, p.name, ge.name, g.releaseDate
                    order by SUM(oi.gameID) desc;";
        private readonly string wishlistQuery = @"
                    SELECT g.Name, ge.name, FORMAT(g.releaseDate, 'MM/dd/yy'), '$' + CAST(g.price AS VARCHAR(15)), p.name,  SUM(wi.gameID)
					FROM Game g 
					JOIN Platform p ON p.platformID = g.platformID
					JOIN Genre ge ON ge.genreID = g.genreID
                    JOIN WishlistItem wi ON g.gameID = wi.gameID
					group by g.name, g.Price, g.Inventory, p.name, ge.name, g.releaseDate
                    order by SUM(wi.gameID) desc;";
        private readonly string salesQuery = @"
                    SELECT g.Name, ge.name, FORMAT(g.releaseDate, 'MM/dd/yy'), p.name, SUM(oi.gameID), '$' + CAST(g.price * SUM(oi.gameID) AS VARCHAR(15))
					FROM Game g 
					JOIN Platform p ON p.platformID = g.platformID
					JOIN Genre ge ON ge.genreID = g.genreID
                    JOIN OrderItem oi ON g.gameID = oi.gameID
					group by g.name, g.Price, g.Inventory, p.name, ge.name, g.releaseDate
                    order by g.price * SUM(oi.gameID) desc;";

        private IConverter _converter;
        public IConfiguration _configuration;
        public string conn = "";

        public ReportController(IConverter converter, IConfiguration configuration)
        {
            _converter = converter;
            _configuration = configuration;
            conn = _configuration.GetConnectionString("Team5_ConestogaVirtualGameStoreContextConnection");

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberDetailPdf()
        {
            var pdf = GetReportFromSQL(memberDetailsQuery, new string[] { "User Name", "Email", "First Name", "Last Name", "Phone Number", "Gender", "Birthday" }, conn).ToPDF("Member Details");
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "MemberDetailsReport.pdf");
        }
        public IActionResult MemberDetailExcel()
        {
            var excel = GetReportFromSQL(memberDetailsQuery, new string[] { "User Name", "Email", "First Name", "Last Name", "Phone Number", "Gender", "Birthday" }, conn).ToExcel("Member Details");
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MemberDetailsReport.xlsx");
        }
        public IActionResult GameDetailPdf()
        {
            var pdf = GetReportFromSQL(gameDetailsQuery, new string[] { "Name", "Release Date", "Price", "Inventory", "Platform", "Genre", "Description" }, conn).ToPDF("Game Details");
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "GameDetailsReport.pdf");
        }
        public IActionResult GameDetailExcel()
        {
            var excel = GetReportFromSQL(gameDetailsQuery, new string[] { "Name", "Release Date", "Price", "Inventory", "Platform", "Genre", "Description" }, conn).ToExcel("Game Details");
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GameDetailsReport.xlsx");
        }
        public IActionResult PopularGamesPdf()
        {
            var pdf = GetReportFromSQL(popularGamesQuery, new string[] { "Name", "Genre", "Release Date", "Price", "Platform", "Order counts", "Total Sales" }, conn).ToPDF("Popular Games");
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "PopularGamesReport.pdf");
        }
        public IActionResult PopularGamesExcel()
        {
            var excel = GetReportFromSQL(popularGamesQuery, new string[] { "Name", "Genre", "Release Date", "Price", "Platform", "Order counts", "Total Sales" }, conn).ToExcel("Popular Games");
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PopularGamesReport.xlsx");
        }
        public IActionResult SalesPdf()
        {
            var pdf = GetReportFromSQL(salesQuery, new string[] { "Name", "Genre", "Release Date", "Platform", "Orders", "Total Sales" }, conn).ToPDF("Sales From Games");
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "SalesReport.pdf");
        }
        public IActionResult SalesExcel()
        {
            var excel = GetReportFromSQL(salesQuery, new string[] { "Name", "Genre", "Release Date", "Platform", "Orders", "Total Sales" }, conn).ToExcel("Sales From Games");
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesReport.xlsx");
        }
        public IActionResult WishlistPdf()
        {
            var pdf = GetReportFromSQL(wishlistQuery, new string[] { "Name", "Genre", "Release Date", "Price", "Platform", "Wishlist counts" }, conn).ToPDF("Wishlist Top Games");
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "WishlistReport.pdf");
        }
        public IActionResult WishlistExcel()
        {
            var excel = GetReportFromSQL(wishlistQuery, new string[] { "Name", "Genre", "Release Date", "Price", "Platform", "Wishlist counts" }, conn).ToExcel("Wishlist Top Games");
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WishlistReport.xlsx");
        }

        private static ReportsBuilder GetReportFromSQL(string queryString, string[] columns, string conn)
        {
            using SqlConnection connection = new SqlConnection(conn);
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            var map = new Dictionary<string, List<string>>();

            while (reader.Read())
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    if (map.ContainsKey(columns[i]))
                    {
                        var list = map[columns[i]];
                        list.Add(reader[i].ToString());
                        map[columns[i]] = list;
                    }
                    else
                    {
                        map[columns[i]] = new List<string> { reader[i].ToString() };
                    }
                }
            }
            reader.Close();

            return new ReportsBuilder(map);
        }
    }

    class ReportsBuilder
    {
        private readonly Dictionary<string, List<string>> report = new Dictionary<string, List<string>>();

        public ReportsBuilder(Dictionary<string, List<string>> report)
        {
            this.report = report;
        }

        public byte[] ToExcel(string reportName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(reportName);
            int columnIndex = 1;
            foreach (var entry in report)
            {
                int rowIndex = 1;

                worksheet.Cell(rowIndex++, columnIndex).Value = entry.Key;
                for (int i = 0; i < entry.Value.Count; i++)
                {
                    worksheet.Cell(rowIndex++, columnIndex).Value = entry.Value[i];
                }
                columnIndex++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream.ToArray();
        }

        public HtmlToPdfDocument ToPDF(string reportName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <p>{1}</p>
                                <div class='header'><h1>{0}</h1></div>
                                <table align='center'>
                                    <tr>", reportName, System.DateTime.Now.ToLongDateString());
            foreach (var column in report.Keys)
            {
                sb.AppendFormat("<th>{0}</th>", column);
            }
            sb.Append("</tr>");

            var columnLength = report.Values.Count;
            var rowLength = report.Values.Max(x => x != null ? x.Count : 0);

            for (int i = 0; i < rowLength; i++)
            {
                sb.Append("<tr>");
                for (int j = 0; j < columnLength; j++)
                {
                    sb.AppendFormat("<td>{0}</td>", report.Values.ElementAt(j)[i]);
                }
                sb.Append("</tr>");
            }

            sb.Append(@"</table></body></html>");

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = reportName
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = sb.ToString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "reports.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return pdf;
        }
    }
}
