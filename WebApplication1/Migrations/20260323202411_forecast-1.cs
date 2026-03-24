using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class forecast1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ApparentTemperature",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DewPoint",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FetchedAt",
                table: "Forecasts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Fog",
                table: "Forecasts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDay",
                table: "Forecasts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Forecasts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Light",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MoonPhase",
                table: "Forecasts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Precipitation",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sunrise",
                table: "Forecasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sunset",
                table: "Forecasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureAvg",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WindDirection",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WindGust",
                table: "Forecasts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WindSpeed",
                table: "Forecasts",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApparentTemperature",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "DewPoint",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "FetchedAt",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Fog",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "IsDay",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Light",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "MoonPhase",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Precipitation",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Sunrise",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Sunset",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "TemperatureAvg",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "WindDirection",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "WindGust",
                table: "Forecasts");

            migrationBuilder.DropColumn(
                name: "WindSpeed",
                table: "Forecasts");
        }
    }
}
