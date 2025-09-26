using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PromoPilot.Application.Interfaces;
using PromoPilot.Application.Mapping; // Your mapping profile namespace
using PromoPilot.Application.Services;
using PromoPilot.Application.UseCases.Budgets;
using PromoPilot.Application.UseCases.CampaignReports;
using PromoPilot.Application.UseCases.CampaignReports.Implementations;
using PromoPilot.Application.UseCases.Campaigns;
using PromoPilot.Application.UseCases.Customers;
using PromoPilot.Application.UseCases.Engagements;
using PromoPilot.Application.UseCases.ExecutionStatuses;
using PromoPilot.Application.UseCases.Products;
using PromoPilot.Application.UseCases.Sales;
using PromoPilot.Core.Interfaces;
using PromoPilot.Infrastructure.Data;
using PromoPilot.Infrastructure.Repositories;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<PromoPilotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
//builder.Services.AddScoped<ICampaignReportService, CampaignReportService>();
builder.Services.AddScoped<IEngagementService, EngagementService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IExecutionStatusService, ExecutionStatusService>();

// Register Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IExecutionStatusRepository, ExecutionStatusRepository>();
builder.Services.AddScoped<IEngagementRepository, EngagementRepository>();
//builder.Services.AddScoped<ICampaignReportRepository, CampaignReportRepository>();
// Register UseCases for Campaigns
builder.Services.AddScoped<IPlanCampaignUseCase, PlanCampaignUseCase>();
builder.Services.AddScoped<IGetAllCampaignsUseCase, GetAllCampaignsUseCase>();
builder.Services.AddScoped<IGetCampaignByIdUseCase, GetCampaignByIdUseCase>();
builder.Services.AddScoped<IUpdateCampaignUseCase, UpdateCampaignUseCase>();
builder.Services.AddScoped<IDeleteCampaignUseCase, DeleteCampaignUseCase>();
builder.Services.AddScoped<ICampaignPlanningUseCase, CampaignPlanningUseCase>();
builder.Services.AddScoped<IPlanCampaignUseCase, PlanCampaignUseCase>();
builder.Services.AddScoped<ICampaignPlanningUseCase, CampaignPlanningUseCase>();
builder.Services.AddScoped<IScheduleCampaignUseCase, ScheduleCampaignUseCase>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


//Register UseCases for Customers
builder.Services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
builder.Services.AddScoped<IGetAllCustomersUseCase, GetAllCustomersUseCase>();
builder.Services.AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
builder.Services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
builder.Services.AddScoped<IDeleteCustomerUseCase, DeleteCustomerUseCase>();

// Register UseCases for Sales
builder.Services.AddScoped<IProcessSaleUseCase, ProcessSaleUseCase>();
builder.Services.AddScoped<IGetAllSalesUseCase, GetAllSalesUseCase>();
builder.Services.AddScoped<IGetSaleByIdUseCase, GetSaleByIdUseCase>();
builder.Services.AddScoped<IUpdateSaleUseCase, UpdateSaleUseCase>();
builder.Services.AddScoped<IDeleteSaleUseCase, DeleteSaleUseCase>();
// Register UseCases for Budgets
builder.Services.AddScoped<ICreateBudgetUseCase, CreateBudgetUseCase>();
builder.Services.AddScoped<IGetAllBudgetsUseCase, GetAllBudgetsUseCase>();
builder.Services.AddScoped<IGetBudgetByIdUseCase, GetBudgetByIdUseCase>();
builder.Services.AddScoped<IUpdateBudgetUseCase, UpdateBudgetUseCase>();
builder.Services.AddScoped<IDeleteBudgetUseCase, DeleteBudgetUseCase>();
// Register UseCases for Engagements
builder.Services.AddScoped<ITrackEngagementUseCase, TrackEngagementUseCase>();
builder.Services.AddScoped<IGetAllEngagementsUseCase, GetAllEngagementsUseCase>();
builder.Services.AddScoped<IGetEngagementByIdUseCase, GetEngagementByIdUseCase>();
builder.Services.AddScoped<IUpdateEngagementUseCase, UpdateEngagementUseCase>();
builder.Services.AddScoped<IDeleteEngagementUseCase, DeleteEngagementUseCase>();
//Register UseCases for Products
builder.Services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
builder.Services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
builder.Services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
builder.Services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
builder.Services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
//Register UseCases for ExecutionStatus
builder.Services.AddScoped<ICreateExecutionStatusUseCase, CreateExecutionStatusUseCase>();
builder.Services.AddScoped<IGetAllExecutionStatusesUseCase, GetAllExecutionStatusesUseCase>();
builder.Services.AddScoped<IGetExecutionStatusByIdUseCase, GetExecutionStatusByIdUseCase>();
builder.Services.AddScoped<IDeleteExecutionStatusUseCase, DeleteExecutionStatusUseCase>();
//Register UseCases for CampaignReport
//builder.Services.AddScoped<ICampaignReportService, CampaignReportService>();
//builder.Services.AddScoped<IGenerateCampaignReportUseCase, GenerateCampaignReportUseCase>();
//builder.Services.AddScoped<IGetAllCampaignReportsUseCase, GetAllCampaignReportsUseCase>();
//builder.Services.AddScoped<IGetCampaignReportByIdUseCase, GetCampaignReportByIdUseCase>();
//builder.Services.AddAutoMapper(typeof(MappingProfile));




builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();
app.UseSerilogRequestLogging(); // Logs HTTP requests


// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers(); // For Web API

app.Run();
