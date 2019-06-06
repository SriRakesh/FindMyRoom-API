using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FindMyRoom.Entities.Models;

namespace FindMyRoom.DataService
{
    public class FindMyRoomDb : DbContext 
    {
        public FindMyRoomDb()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=orchardsqlserver.database.windows.net;Database=Orchard4;Trusted_Connection=false;MultipleActiveResultSets=true;user id=sqluser4; password=pwd#Login4");

        }
        public FindMyRoomDb(DbContextOptions<FindMyRoomDb> options) : base(options)
        { }

        public DbSet<User> FMRUsers { get; set; }
        public DbSet<Room> FMRRooms { get; set; }
        public DbSet<Book> FMRBookings { get; set; }
        public DbSet<Owner> FMROwners { get; set; }
        public DbSet<WishList> FMRWishLists { get; set; }
        public DbSet<GeoLocation> FMRGeolocation { get; set; }
        public DbSet<SocialLogin> FMRSociallogin { get; set; }
        public DbSet<Image> FMRImages { get; set; }
    }
}
