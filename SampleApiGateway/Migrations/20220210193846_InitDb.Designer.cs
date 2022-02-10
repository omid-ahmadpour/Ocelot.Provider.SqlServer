﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ocelot.Provider.SqlServer.Db;

#nullable disable

namespace SampleApiGateway.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220210193846_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ocelot.Provider.SqlServer.Models.OcelotGlobalConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BaseUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DownstreamScheme")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GatewayName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("HttpHandlerOptions")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoadBalancerOptions")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("QoSOptions")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("RequestIdKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ServiceDiscoveryProvider")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("OcelotGlobalConfigurations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GatewayName = "TestGateway"
                        });
                });

            modelBuilder.Entity("Ocelot.Provider.SqlServer.Models.OcelotRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OcelotRoutes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Route = "{\r\n  \"DownstreamPathTemplate\": \"/{everything}\",\r\n  \"DownstreamScheme\": \"http\",\r\n  \"DownstreamHostAndPorts\": [\r\n    {\r\n      \"Host\": \"localhost\",\r\n      \"Port\": 5095\r\n    }\r\n  ],\r\n  \"UpstreamPathTemplate\": \"/gateway/{everything}\",\r\n  \"UpstreamHttpMethod\": [ \"Get\" ]\r\n}"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}