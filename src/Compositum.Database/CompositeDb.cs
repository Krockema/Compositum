using System.Linq;
using System.Threading.Tasks;
using Compositum.Database.Tables;
using Microsoft.EntityFrameworkCore;


namespace Compositum.Database
{
    public class CompositeDb : DbContext
    {
        public static CompositeDb GetContext(string connectionString)
        {
            return new CompositeDb(options: new DbContextOptionsBuilder<CompositeDb>()
                .UseSqlServer(connectionString: connectionString)
                .Options);
        }
        public CompositeDb(DbContextOptions<CompositeDb> options) : base(options: options) { }

        public DbSet<Composite> Composites { get; set; }

        // public DbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Composite>()
                .HasDiscriminator<string>("composite_type")
                .HasValue<ComplexCompositeAlternative>("Alternative")
                .HasValue<ComplexCompositeSequence>("Sequence")
                .HasValue<ComplexCompositeParallel>("Parallel")
                .HasValue<ElementaryComposite>("Finite");

            modelBuilder.Entity<Composite>()
                .HasOne(x => x.Successor)
                .WithOne(x => x.Predecessor)
                .HasForeignKey<Composite>(x => x.PredecessorId);

            //modelBuilder.Entity<Relationship>()
            //    .HasOne(x => x.Predecessor)
            //    .WithOne(x => x.Successor)
            //    .OnDelete(DeleteBehavior.Restrict);
            //
            //modelBuilder.Entity<Relationship>()
            //    .HasOne(x => x.Successor)
            //    .WithOne(x => x.Predecessor)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Composite>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public async Task<Composite> GetStructureRecursively(Composite parent, int componentId)
        {
            parent.Children = Composites.Include(x => x.Successor)
                                        .Include(x => x.Predecessor)
                                        .Include(navigationPropertyPath: a => a.Children)
                                            .Where(predicate: a => a.ParentId == componentId)
                                            .ToList();

            if (parent is ComplexCompositeSequence)
                ((ComplexCompositeSequence) parent).SortIfPossible();


            foreach (var item in parent.Children)
            {
                await GetStructureRecursively(parent: item, componentId: item.Id);
            }
            await Task.Yield();
            return parent;

        }

        public static void DbSeed(CompositeDb context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var root = new ComplexCompositeSequence("root");
            var seqencial_1 = new ComplexCompositeSequence("OpenShop");
            var seqencial_2 = new ComplexCompositeSequence("JobShop");
            var alternative_3 = new ComplexCompositeAlternative("Alternative");
            root.Children.Add(seqencial_1);
            root.Children.Add(seqencial_2);
            root.Children.Add(alternative_3);

            seqencial_1.Children.Add(new ElementaryComposite("OS : x"));
            seqencial_1.Children.Add(new ElementaryComposite("OS : y"));
            seqencial_1.Children.Add(new ElementaryComposite("OS : z"));

            var finiteComposite_1 = new ElementaryComposite("JS : one");
            
            var finiteComposite_2 = new ElementaryComposite("JS : two");
            var finiteComposite_3 = new ElementaryComposite("JS : three");

            seqencial_2.Children.Add(finiteComposite_1);
            seqencial_2.Children.Add(finiteComposite_2);
            seqencial_2.Children.Add(finiteComposite_3);
            

            alternative_3.Children.Add(new ElementaryComposite("Alternative: one"));
            var seqencial_4 = new ComplexCompositeSequence("Alternative: two");
            alternative_3.Children.Add(seqencial_4);

            var finiteComposite_4 = new ElementaryComposite("JS: five");
            var finiteComposite_5 = new ElementaryComposite("JS: six");
            seqencial_4.Children.Add(finiteComposite_4);
            seqencial_4.Children.Add(finiteComposite_5);

            // set hirachie 

            seqencial_2.Predecessor = seqencial_1;
            alternative_3.Predecessor = seqencial_2;

            finiteComposite_2.Predecessor = finiteComposite_1;
            finiteComposite_3.Predecessor = finiteComposite_2;
            finiteComposite_5.Predecessor = finiteComposite_4;

            context.Add(root);
            context.SaveChanges();


            //var rs1 = new Relationship {Predecessor = seqencial_1, Successor = seqencial_2};
            //var rs2 = new Relationship {Predecessor = seqencial_2, Successor = alternative_3};
            //var rs3 = new Relationship {Predecessor = finiteComposite_1, Successor = finiteComposite_2};
            //var rs4 = new Relationship {Predecessor = finiteComposite_2, Successor = finiteComposite_3};
            //var rs5 = new Relationship {Predecessor = finiteComposite_4, Successor = finiteComposite_5};
            //
            //context.Relationships.AddRange(new List<Relationship>() {rs1, rs2, rs3, rs4, rs5});
            //context.SaveChanges();
        }

    }
}
