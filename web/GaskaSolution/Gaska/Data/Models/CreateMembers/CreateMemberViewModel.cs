using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Gaska.Models.CreateMembers
{
    public class CreateMemberViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Game")]
        public string game { get; set; }

        [Required]
        [Display(Name = "Server")]
        public string server { get; set; }

        [Required]
        [Display(Name = "Guild Name")]
        public string guildName { get; set; }

        [Display(Name = "If you are the Guild Admin for this site, check this box")]
        public bool guildAdmin { get; set; }

        [Display(Name = "If you are the Guild Leader, check this box")]
        public bool guildLeader { get; set; }


        public string guildMemberAuthenticationTokenNumber { get; set; }

        [Required]
        [Display(Name = "Main Character Name")]
        public string CharacterName1 { get; set; }


        [Display(Name = "Alt Character Name")]
        public string CharacterName2 { get; set; }


        [Display(Name = "Alt Character Name")]
        public string CharacterName3 { get; set; }


        [Display(Name = "Alt Character Name")]
        public string CharacterName4 { get; set; }


        [Display(Name = "Alt Character Name")]
        public string CharacterName5 { get; set; }
    }
}
