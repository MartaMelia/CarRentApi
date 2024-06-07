﻿global using CarRent.Api.Common.Errors;
global using CarRent.Api.Common.Http;
global using CarRent.Api.Common.Swagger;
global using CarRent.Api.Dependencies;
global using CarRent.Application.Common.Models;
global using CarRent.Application.Dependencies;
global using CarRent.Application.Features.Cars.Common;
global using CarRent.Application.Features.Cars.Queries.GetCar;
global using CarRent.Application.Features.Cars.Queries.GetCars;
global using CarRent.Application.Features.Cars.Queries.RandomCars;
global using CarRent.Application.Features.Users.Commands.Login;
global using CarRent.Application.Features.Users.Commands.Logout;
global using CarRent.Application.Features.Users.Commands.Register;
global using CarRent.Application.Features.Users.Common;
global using CarRent.Infrastructure.Dependencies;
global using ErrorOr;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using System.Diagnostics;
global using System.Security.Claims;
