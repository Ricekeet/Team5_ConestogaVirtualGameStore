using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_ConestogaVirtualGameStore.ViewModels
{
    public class CardModel
    {
        [Required, Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Required, CreditCard, Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required, Display(Name = "Expiration Month"), Range(0, 12)]
        public int CardExpiryMonth { get; set; }

        [Required, Display(Name = "Expiration Year"), Range(2020, 2100)]
        public int CardExpiryYear { get; set; }

        [Required, Display(Name = "CVC Security Code"),
            RegularExpression("^[0-9]*$", ErrorMessage = "CVC security code can only contain numbers")]
        public string CardCvc { get; set; }
    }
}
