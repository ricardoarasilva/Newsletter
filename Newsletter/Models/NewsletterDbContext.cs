namespace Newsletter.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class NewsletterDbContext : DbContext
    {
        // Your context has been configured to use a 'NewsletterDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Newsletter.Models.NewsletterDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'NewsletterDbContext' 
        // connection string in the application configuration file.
        public NewsletterDbContext()
            : base("name=NewsletterDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<NewsLetterSign> NewsLetterSigns { get; set; }
    }

    public class NewsLetterSign
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Invalid E-mail!")]
        [Required]
        [Remote("DoesEmailExists","Newsletter",HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different user name.")]
        public string Email { get; set; }
        [Display(Name = "How Heard About Us")]
        [Required]
        public HowHeardAbout HowHeardAboutUs { get; set; }
        [Display(Name = "Reason To Signing Up")]
        [DataType(DataType.MultilineText)]
        public string ReasonToSigningUp { get; set; }
    }

    public enum HowHeardAbout
    {
        [Display(Name = "Advert")]
        Advert,
        [Display(Name = "Word Of Mouth")]
        WordOfMouth,
        [Display(Name = "Other")]
        Other
    }
}