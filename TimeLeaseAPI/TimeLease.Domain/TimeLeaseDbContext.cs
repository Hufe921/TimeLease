using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLease.Domain.Entities;

namespace TimeLease.Domain
{
    public class TimeLeaseDbContext:DbContext
    {
        public TimeLeaseDbContext() : base("TimeLease")
        {
        }
        public virtual DbSet<UserEntity> UserEntity { get; set; }
        public virtual DbSet<Apply> Apply { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleDetail> ArticleDetail { get; set; }
        public virtual DbSet<ArticleLook> ArticleLook { get; set; }
        public virtual DbSet<ArticlePraise> ArticlePraise { get; set; }
        public virtual DbSet<BonusPoint> BonusPoint { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TypeEntity> TypeEntity { get; set; }

        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShopType> ShopType { get; set; }
        public virtual DbSet<ShopExchange> ShopExchange { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new ApplyConfig());
            modelBuilder.Configurations.Add(new ArticleConfig());
            modelBuilder.Configurations.Add(new ArticleDetailConfig());
            modelBuilder.Configurations.Add(new ArticleLookConfig());
            modelBuilder.Configurations.Add(new ArticlePraiseConfig());
            modelBuilder.Configurations.Add(new BonusPointConfig());
            modelBuilder.Configurations.Add(new CommentConfig());
            modelBuilder.Configurations.Add(new TagConfig());
            modelBuilder.Configurations.Add(new TypeConfig());
            modelBuilder.Configurations.Add(new ShopConfig());
            modelBuilder.Configurations.Add(new ShopTypeConfig());
            modelBuilder.Configurations.Add(new ShopExchangeConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
