using Murtain.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Localization.Models
{
    public class LanguageText : Entity<Guid>
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        [JsonIgnore]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }
        /// <summary>
        /// Language name (culture name). Matches to <see cref="ApplicationLanguage.Name"/>.
        /// </summary>
        [Required]
        [StringLength(10)]
        public virtual string LanguageName { get; set; }
        /// <summary>
        /// Localization source name
        /// </summary>
        [Required]
        [StringLength(128)]
        public virtual string Source { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [Required]
        [StringLength(256)]
        public virtual string Key { get; set; }

        /// <summary>
        /// Localized value
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [StringLength(64 * 1024 * 1024)]
        public virtual string Value { get; set; }
    }
}
