﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FastighetsProjectApi_CCRA.Model;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<RealEstate> RealEstates { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<User>().HasData
          (
              new User
              {
                  ID = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                  UserId = "hejj",
                  UserName = "Börje@shietshow.com",
                  RealEstates = 2,
                  Comments = 1,
                  Rating = 4
              },
              new User
              {
                  ID = new Guid("c9d4c053-49b6-410c-bc78-2d54a999187c"),
                  UserId = "hallå",
                  UserName = "Börje@shietshow.com",
                  RealEstates = 3,
                  Comments = 2,
                  Rating = 5
              }

          );
        modelBuilder.Entity<RealEstate>().HasData
          (
              new RealEstate
              {
                  Contact = "nils",
                  CreatedOn = new DateTime(2020 - 10 - 12),
                  ConstructionYear = 1710,
                  Address = "Hallalundavägen 12",
                  Type = 2,
                  Description = "Lägenhetshus",
                  Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991780"),
                  ide = 1,
                  Title = "titel1",
                  SellingPrice = 120000,
                  CanBeRented = false,
                  CanBeSold = true,
                  UserName = "Björn",
                  RealestateType = "Lägenhetshus",


              },
              new RealEstate
              {
                  Contact = "maria",
                  CreatedOn = new DateTime(2020 - 10 - 14),
                  ConstructionYear = 1850,
                  Address = "Hamburgarevägen 12",
                  Type = 3,
                  Description = "Lagerlokal",
                  Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991799"),
                  ide = 2,
                  Title = "titel2",
                  SellingPrice = 2000000,
                  CanBeRented = false,
                  CanBeSold = true,
                  UserName = "Nisse",
                  RealestateType = "Lagerlokal"
              }
           ) ;
        modelBuilder.Entity<Comment>().HasData
        (
            new Comment
            {
                GuidID = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991798"),
                id = 1,
                RealEstateIde = 1,
                Content = "Superbra",
                UserName = "Björn",
                CreatedOn = new DateTime(2020 - 12 - 13)

            },
            new Comment
            {
                GuidID = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991797"),
                id = 2,
                RealEstateIde = 2,
                Content = "Superbra men jättedåligt",
                UserName = "Nisse",
                CreatedOn = new DateTime(2020 - 12 - 14)
            }

        );
    }
        }
