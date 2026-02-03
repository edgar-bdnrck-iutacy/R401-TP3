using TP3Console.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TP3Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new FilmsDbContext())
            {
                // Désactivation du tracking => Aucun changement dans la base de données ne sera effectué
                // ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                // SELECT
                //                       AsNoTracking() => Désactive le tracking pour cette requête seulement
                Film titanic = ctx.Films.AsNoTracking().First(f => f.Nom.Contains("Titanic"));

                // Modification de l'entité (dans le contexte seulement)
                titanic.Description = "Un bateau qui coule. Date : " + DateTime.Now;

                // Sauvegarde du contexte => Application de la modification dans la BD
                int nbchanges = ctx.SaveChanges();

                Console.WriteLine($"Nombre de changements appliqués à la BD : {nbchanges}");
            }

            //using (var ctx = new FilmsDbContext())
            //{
            //    Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
            //    Console.WriteLine("Categorie : " + categorieAction.Nom);

            //    //Chargement des films dans categorieAction
            //    ctx.Entry(categorieAction).Collection(c => c.Films).Load();
            //    Console.WriteLine("Films : ");
            //    foreach (var film in categorieAction.Films)
            //    {
            //        Console.WriteLine(film.Nom);
            //    }
            //}

            // Chargement hatif
            //using (var ctx = new FilmsDbContext())
            //{
            //    //Chargement de la catégorie Action et des films de cette catégorie
            //    Categorie categorieAction = ctx.Categories
            //                                .Include(c => c.Films)
            //                                .First(c => c.Nom == "Action");

            //    Console.WriteLine("Categorie : " + categorieAction.Nom);
            //    Console.WriteLine("Films : ");
            //    foreach (var film in categorieAction.Films)
            //    {
            //        Console.WriteLine(film.Nom);
            //    }
            //}

            using (var ctx = new FilmsDbContext())
            {
                //Chargement de la catégorie Action, des films de cette catégorie et des avis
                Categorie categorieAction = ctx.Categories
                                                .Include(c => c.Films)
                                                    .ThenInclude(f => f.Avis)
                                                .First(c => c.Nom == "Action");
            }
        }
    }
}
