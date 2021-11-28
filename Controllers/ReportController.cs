using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Areas.Identity.Data;
using Team5_ConestogaVirtualGameStore.Data;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class ReportController : Controller
    {
        private readonly CVGS_Context _context;
        private readonly CVGS_IdentityContext identityDb;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<CVGS_User> userManager;
        private IConverter _converter;

        public ReportController(CVGS_Context context, UserManager<CVGS_User> userManager, IConverter converter, RoleManager<IdentityRole> roleManager, CVGS_IdentityContext identityDb)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _converter = converter;
            this.identityDb = identityDb;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DownloadPdf()
        {
            var users = new List<CVGS_User>();


            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, "Member"))
                {
                    users.Add(user);
                }
            }
            return View(users);
        }

        public async Task<IActionResult> DownloadMemberDetailPdf()
        {
            var users = new List<CVGS_User>();
            

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, "Member"))
                {
                    users.Add(user);
                }
            }
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>UserName</th>
                                        <th>Email</th>
                                        <th>First Name</th>
                                        <th>Last Name</th>
                                    </tr>");
            foreach (var user in users)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", user.UserName, user.Email, user.FirstName, user.LastName);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");


            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
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
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "MemberDetailsReport.pdf");
        }

        public async Task<IActionResult> DownloadMemberDetailExcel()
        {
            var users = new List<CVGS_User>();
            //var members = (from u in identityDb.Users
            //               join ur in identityDb.UserRoles on u.Id equals ur.UserId
            //               join r in identityDb.Roles.Where(x => x.NormalizedName == "MEMBERS") on ur.RoleId equals r.Id
            //               select new { 
            //               userId = u.Id,
            //               roleId = r.Id,
            //               firstName = u.FirstName,
            //               lastName = u.LastName,
            //               phone = u.PhoneNumber
            //               }).ToList();


            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, "Member"))
                {
                    users.Add(user);
                }
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MemberDetail");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Username";
                worksheet.Cell(currentRow, 2).Value = "Email";
                worksheet.Cell(currentRow, 3).Value = "First Name";
                worksheet.Cell(currentRow, 4).Value = "Last Name";
                worksheet.Cell(currentRow, 5).Value = "Phone Number";
                worksheet.Cell(currentRow, 6).Value = "Address";
                worksheet.Cell(currentRow, 7).Value = "Favorite Genre";
                worksheet.Cell(currentRow, 8).Value = "Favorite Platform";

                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.UserName;
                    worksheet.Cell(currentRow, 2).Value = user.Email;
                    worksheet.Cell(currentRow, 3).Value = user.FirstName;
                    worksheet.Cell(currentRow, 4).Value = user.LastName;
                    worksheet.Cell(currentRow, 5).Value = user.PhoneNumber;
                    worksheet.Cell(currentRow, 6).Value = user.AddressListID;
                    worksheet.Cell(currentRow, 7).Value = user.FavoriteGenreID;
                    worksheet.Cell(currentRow, 8).Value = user.FavoritePlatformID;
                }
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "MemberDetail.xlsx");
                }
            }
        }
    }
}
