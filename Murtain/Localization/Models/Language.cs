using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Murtain.Localization.Language;

namespace Murtain.Localization.Models
{
    public class Language : Entity<Guid>
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        [JsonIgnore]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the culture, like "en" or "en-US".
        /// </summary>
        [Required]
        [StringLength(10)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Required]
        [StringLength(128)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        [StringLength(128)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// Map to LanguageInfo.
        /// </summary>
        /// <returns></returns>
        public virtual LanguageInfo MapToLanguageInfo()
        {
            return new LanguageInfo(Name, DisplayName, Icon);
        }
    }
}
