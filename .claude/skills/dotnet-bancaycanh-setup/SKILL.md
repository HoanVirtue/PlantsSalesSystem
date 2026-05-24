---
name: dotnet-bancaycanh-setup
description: Setup ASP.NET Core API for BanCayCanh with CQRS + Repository pattern + MySQL + Elasticsearch + MinIO
disable-model-invocation: false
allowed-tools: Bash(dotnet *) Bash(git *)
---

# Setup ASP.NET Core Backend for BanCayCanh

## Project Architecture
- **Pattern**: CQRS with MediatR + Generic Repository pattern
- **Database**: MySQL 8 with EF Core Pomelo provider
- **Search**: Elasticsearch for product search
- **Storage**: MinIO for plant images
- **Principles**: SOLID (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion)

## Folder Structure
```
source/BE/BanCayCanh.API/
├── BanCayCanh.API.sln
├── src/
│   ├── BanCayCanh.API/                  # Controllers, Middleware, DI setup
│   │   ├── Controllers/
│   │   │   ├── PlantsController.cs
│   │   │   ├── OrdersController.cs
│   │   │   ├── CustomersController.cs
│   │   │   ├── AuthController.cs
│   │   │   ├── ChatbotController.cs
│   │   │   └── UploadController.cs
│   │   ├── Middleware/
│   │   │   ├── GlobalExceptionMiddleware.cs
│   │   │   └── JwtMiddleware.cs
│   │   ├── Program.cs                   # DI container, Swagger, CORS
│   │   ├── appsettings.json
│   │   └── appsettings.Development.json
│   │
│   ├── BanCayCanh.Application/          # CQRS: Commands, Queries, Handlers
│   │   ├── Features/
│   │   │   ├── Plants/
│   │   │   │   ├── Commands/
│   │   │   │   │   ├── CreatePlantCommand.cs
│   │   │   │   │   ├── UpdatePlantCommand.cs
│   │   │   │   │   ├── DeletePlantCommand.cs
│   │   │   │   │   └── UpdatePlantStatusCommand.cs
│   │   │   │   ├── Queries/
│   │   │   │   │   ├── GetPlantsQuery.cs
│   │   │   │   │   ├── GetPlantByIdQuery.cs
│   │   │   │   │   ├── GetPlantBySlugQuery.cs
│   │   │   │   │   ├── SearchPlantsQuery.cs
│   │   │   │   │   └── GetFeaturedPlantsQuery.cs
│   │   │   │   └── Handlers/
│   │   │   │       ├── CreatePlantCommandHandler.cs
│   │   │   │       ├── UpdatePlantStatusCommandHandler.cs
│   │   │   │       ├── GetPlantsQueryHandler.cs
│   │   │   │       └── SearchPlantsQueryHandler.cs
│   │   │   │
│   │   │   ├── Orders/
│   │   │   │   ├── Commands/
│   │   │   │   │   ├── CreateOrderCommand.cs
│   │   │   │   │   └── UpdateOrderStatusCommand.cs
│   │   │   │   ├── Queries/
│   │   │   │   │   ├── GetOrdersQuery.cs
│   │   │   │   │   └── GetOrderByIdQuery.cs
│   │   │   │   └── Handlers/
│   │   │   │
│   │   │   ├── Customers/
│   │   │   │   ├── Commands/
│   │   │   │   │   ├── UpsertCustomerCommand.cs
│   │   │   │   │   └── UpdateCustomerTypeCommand.cs
│   │   │   │   ├── Queries/
│   │   │   │   │   ├── GetCustomersQuery.cs
│   │   │   │   │   └── GetCustomerByIdQuery.cs
│   │   │   │   └── Handlers/
│   │   │   │
│   │   │   ├── Chatbot/
│   │   │   │   ├── Commands/
│   │   │   │   │   └── SendChatbotMessageCommand.cs
│   │   │   │   └── Handlers/
│   │   │   │
│   │   │   └── Auth/
│   │   │       ├── Commands/
│   │   │       │   └── LoginCommand.cs
│   │   │       └── Handlers/
│   │   │
│   │   ├── DTOs/
│   │   │   ├── Plant/
│   │   │   │   ├── CreatePlantDto.cs
│   │   │   │   ├── UpdatePlantDto.cs
│   │   │   │   └── PlantResponseDto.cs
│   │   │   ├── Order/
│   │   │   ├── Customer/
│   │   │   ├── Auth/
│   │   │   └── Common/
│   │   │       └── ApiResponseDto.cs
│   │   │
│   │   ├── Interfaces/
│   │   │   ├── IUnitOfWork.cs
│   │   │   ├── IRepository.cs
│   │   │   ├── IMinioService.cs
│   │   │   ├── IElasticsearchService.cs
│   │   │   ├── ITelegramService.cs
│   │   │   └── IChatbotService.cs
│   │   │
│   │   ├── Mappings/
│   │   │   └── MappingProfile.cs        # AutoMapper profiles
│   │   │
│   │   └── ApplicationServiceCollectionExtension.cs
│   │
│   ├── BanCayCanh.Domain/               # Entities, Enums, Value Objects
│   │   ├── Entities/
│   │   │   ├── Plant.cs
│   │   │   ├── PlantImage.cs
│   │   │   ├── Order.cs
│   │   │   ├── Customer.cs
│   │   │   ├── PriceRange.cs
│   │   │   ├── AdminUser.cs
│   │   │   └── ChatbotSession.cs
│   │   │
│   │   ├── Enums/
│   │   │   ├── TreeShapeEnum.cs         # BanTra, Truc, TanRong
│   │   │   ├── PotStyleEnum.cs          # Tron, Vuong, ChuNhat
│   │   │   ├── PlantStatusEnum.cs       # Available, Reserved, Sold
│   │   │   ├── OrderStatusEnum.cs       # Pending, Confirmed, Cancelled
│   │   │   └── CustomerTypeEnum.cs      # Regular, VIP, Wholesale
│   │   │
│   │   └── ValueObjects/
│   │       ├── Money.cs
│   │       └── PhoneNumber.cs
│   │
│   ├── BanCayCanh.Infrastructure/       # EF Core, Repositories, Services
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs
│   │   │   ├── Configurations/          # Entity fluent mappings
│   │   │   │   ├── PlantConfiguration.cs
│   │   │   │   ├── OrderConfiguration.cs
│   │   │   │   └── ...
│   │   │   └── Migrations/              # EF migrations
│   │   │
│   │   ├── Repositories/
│   │   │   ├── GenericRepository.cs      # T : IEntity
│   │   │   ├── PlantRepository.cs        # IPlantRepository : IRepository<Plant>
│   │   │   ├── OrderRepository.cs
│   │   │   ├── CustomerRepository.cs
│   │   │   └── UnitOfWork.cs             # IUnitOfWork orchestrator
│   │   │
│   │   ├── Services/
│   │   │   ├── MinioStorageService.cs    # Upload, Download, Delete, PresignedURL
│   │   │   ├── ElasticsearchService.cs   # Index, Search plants
│   │   │   ├── TelegramNotificationService.cs
│   │   │   ├── ChatbotService.cs         # Claude API integration
│   │   │   ├── AuthService.cs            # JWT, password hashing
│   │   │   └── SlugGeneratorService.cs
│   │   │
│   │   ├── ExternalClients/
│   │   │   ├── MinioClient.cs
│   │   │   ├── ElasticsearchClient.cs
│   │   │   ├── TelegramBotClient.cs
│   │   │   └── AnthropicClient.cs        # Claude API
│   │   │
│   │   └── InfrastructureServiceCollectionExtension.cs
│   │
│   └── BanCayCanh.Tests/                # Unit & Integration tests
│       ├── Unit/
│       │   ├── Domain/
│       │   └── Application/
│       └── Integration/
│           └── Api/
```

## Implementation Steps

### Step 1: Create Solution & Projects
```bash
cd source/BE/BanCayCanh.API
dotnet new sln -n BanCayCanh.API
dotnet new classlib -n BanCayCanh.Domain
dotnet new classlib -n BanCayCanh.Application
dotnet new classlib -n BanCayCanh.Infrastructure
dotnet new webapi -n BanCayCanh.API
dotnet new xunit -n BanCayCanh.Tests

# Add projects to solution
dotnet sln BanCayCanh.API.sln add src/BanCayCanh.Domain/BanCayCanh.Domain.csproj
dotnet sln BanCayCanh.API.sln add src/BanCayCanh.Application/BanCayCanh.Application.csproj
dotnet sln BanCayCanh.API.sln add src/BanCayCanh.Infrastructure/BanCayCanh.Infrastructure.csproj
dotnet sln BanCayCanh.API.sln add src/BanCayCanh.API/BanCayCanh.API.csproj
dotnet sln BanCayCanh.API.sln add src/BanCayCanh.Tests/BanCayCanh.Tests.csproj

# Add project references
cd src/BanCayCanh.Application
dotnet add reference ../BanCayCanh.Domain/BanCayCanh.Domain.csproj

cd ../BanCayCanh.Infrastructure
dotnet add reference ../BanCayCanh.Domain/BanCayCanh.Domain.csproj
dotnet add reference ../BanCayCanh.Application/BanCayCanh.Application.csproj

cd ../BanCayCanh.API
dotnet add reference ../BanCayCanh.Application/BanCayCanh.Application.csproj
dotnet add reference ../BanCayCanh.Infrastructure/BanCayCanh.Infrastructure.csproj

cd ../BanCayCanh.Tests
dotnet add reference ../BanCayCanh.Domain/BanCayCanh.Domain.csproj
dotnet add reference ../BanCayCanh.Application/BanCayCanh.Application.csproj
dotnet add reference ../BanCayCanh.Infrastructure/BanCayCanh.Infrastructure.csproj
dotnet add reference ../BanCayCanh.API/BanCayCanh.API.csproj
```

### Step 2: Install NuGet Packages
```bash
# Infrastructure layer - Data access
cd src/BanCayCanh.Infrastructure
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0

# Services
dotnet add package Minio --version 1.0.7
dotnet add package Elastic.Clients.Elasticsearch --version 8.0.0
dotnet add package Telegram.Bot --version 19.0.0
dotnet add package Anthropic.Sdk --version 2.2.0
dotnet add package BCrypt.Net-Next --version 4.0.3
dotnet add package AutoMapper --version 13.0.0

# Application layer - CQRS
cd ../BanCayCanh.Application
dotnet add package MediatR --version 12.1.1
dotnet add package FluentValidation --version 11.7.0
dotnet add package AutoMapper --version 13.0.0

# API layer
cd ../BanCayCanh.API
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0
dotnet add package Swashbuckle.AspNetCore --version 6.4.0
dotnet add package Serilog.AspNetCore --version 8.0.0
dotnet add package Serilog.Sinks.Console --version 5.0.0

# Tests
cd ../BanCayCanh.Tests
dotnet add package Moq --version 4.20.0
dotnet add package FluentAssertions --version 6.12.0
```

### Step 3: Create Domain Entities
Implement all entities in `BanCayCanh.Domain/Entities/` with:
- Base entity with Id, CreatedAt, UpdatedAt
- Navigation properties for relationships
- Value object support

### Step 4: Create DbContext & EF Migrations
```bash
cd src/BanCayCanh.Infrastructure
dotnet ef migrations add InitialCreate -p ../BanCayCanh.API/BanCayCanh.API.csproj
dotnet ef database update -p ../BanCayCanh.API/BanCayCanh.API.csproj
```

### Step 5: Implement Generic Repository Pattern
```csharp
// IRepository<TEntity> interface with SOLID principles
// - Single Responsibility: only data access
// - Open/Closed: extensible via inheritance, closed for modification
// - Liskov Substitution: derived repos behave like base
// - Interface Segregation: small focused interfaces
// - Dependency Inversion: depend on abstractions

// GenericRepository<TEntity> implementation
// - CRUD operations
// - Query with filtering, sorting, pagination
// - Async/await throughout

// IUnitOfWork orchestrator
// - Manages multiple repositories
// - Transaction scope (SaveChangesAsync)
```

### Step 6: Implement CQRS with MediatR
```csharp
// Command = write operation
// - CreatePlantCommand
// - UpdatePlantStatusCommand
// Handler validates, executes, commits

// Query = read operation
// - GetPlantsQuery
// - SearchPlantsQuery
// Handler executes, returns DTO

// Use MediatR pipeline behaviors for:
// - Validation (FluentValidation)
// - Logging
// - Performance monitoring
```

### Step 7: Create Services
- **MinioStorageService**: Upload, GetPresignedUrl, Delete
- **ElasticsearchService**: Index plants, Search with filters
- **TelegramNotificationService**: Send order notifications
- **ChatbotService**: Integrate Claude API for Vietnamese plant advisory
- **AuthService**: JWT generation, password hashing with BCrypt

### Step 8: Create Controllers
- **PlantsController**: GET /plants, GET /plants/{id}, POST /admin/plants, etc.
- **OrdersController**: POST /orders, GET /admin/orders
- **CustomersController**: GET /admin/customers
- **AuthController**: POST /auth/login, POST /auth/refresh
- **ChatbotController**: POST /chatbot/message
- **UploadController**: POST /admin/upload/presigned-url

### Step 9: Configure Dependency Injection
In `Program.cs`:
```csharp
// Add infrastructure services
services.AddInfrastructure(configuration);

// Add application services (CQRS, AutoMapper, Validation)
services.AddApplication();

// Add controllers, Swagger, CORS, JWT auth
services.AddControllers();
services.AddSwaggerGen();
services.AddCors(...);
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)...
```

### Step 10: Add Global Exception Middleware
- Catch all exceptions
- Return standardized ApiResponse<T>
- Log with Serilog
- Different HTTP status codes for different errors

## Generic Repository Pattern - Chi tiết Implementation

### 1. Base Entity Interface
```csharp
// BanCayCanh.Domain/Entities/IEntity.cs
public interface IEntity
{
    int Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
```

### 2. IRepository<T> Interface - Chỉ CRUD cơ bản
```csharp
// BanCayCanh.Application/Interfaces/IRepository.cs
public interface IRepository<T> where T : class, IEntity
{
    // CREATE
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    // READ
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    // UPDATE
    T Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    // DELETE
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    // Utilities
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> AsQueryable();
}
```

### 3. Generic Repository Implementation
```csharp
// BanCayCanh.Infrastructure/Repositories/GenericRepository.cs
public class GenericRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // CREATE
    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        var now = DateTime.UtcNow;
        foreach (var entity in entities)
        {
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
        }
        await _dbSet.AddRangeAsync(entities);
    }

    // READ
    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);

    // UPDATE
    public T Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        return entity;
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        var now = DateTime.UtcNow;
        foreach (var entity in entities)
            entity.UpdatedAt = now;
        _dbSet.UpdateRange(entities);
    }

    // DELETE
    public void Remove(T entity)
        => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
        => _dbSet.RemoveRange(entities);

    // UTILITIES
    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        => predicate == null 
            ? await _dbSet.CountAsync() 
            : await _dbSet.CountAsync(predicate);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    public IQueryable<T> AsQueryable()
        => _dbSet.AsQueryable();
}
```

### 4. IUnitOfWork - Quản lý tất cả Repository
```csharp
// BanCayCanh.Application/Interfaces/IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    IRepository<Plant> Plants { get; }
    IRepository<Order> Orders { get; }
    IRepository<Customer> Customers { get; }
    IRepository<PlantImage> PlantImages { get; }
    IRepository<PriceRange> PriceRanges { get; }
    IRepository<AdminUser> AdminUsers { get; }
    IRepository<ChatbotSession> ChatbotSessions { get; }

    Task<int> SaveChangesAsync();
    Task<bool> BeginTransactionAsync();
    Task<bool> CommitTransactionAsync();
    Task<bool> RollbackTransactionAsync();
}
```

### 5. UnitOfWork Implementation
```csharp
// BanCayCanh.Infrastructure/Repositories/UnitOfWork.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    private IRepository<Plant>? _plants;
    private IRepository<Order>? _orders;
    private IRepository<Customer>? _customers;
    private IRepository<PlantImage>? _plantImages;
    private IRepository<PriceRange>? _priceRanges;
    private IRepository<AdminUser>? _adminUsers;
    private IRepository<ChatbotSession>? _chatbotSessions;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    // Lazy-load repositories
    public IRepository<Plant> Plants
        => _plants ??= new GenericRepository<Plant>(_context);

    public IRepository<Order> Orders
        => _orders ??= new GenericRepository<Order>(_context);

    public IRepository<Customer> Customers
        => _customers ??= new GenericRepository<Customer>(_context);

    public IRepository<PlantImage> PlantImages
        => _plantImages ??= new GenericRepository<PlantImage>(_context);

    public IRepository<PriceRange> PriceRanges
        => _priceRanges ??= new GenericRepository<PriceRange>(_context);

    public IRepository<AdminUser> AdminUsers
        => _adminUsers ??= new GenericRepository<AdminUser>(_context);

    public IRepository<ChatbotSession> ChatbotSessions
        => _chatbotSessions ??= new GenericRepository<ChatbotSession>(_context);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task<bool> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return true;
    }

    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
            return true;
        }
        catch
        {
            await RollbackTransactionAsync();
            return false;
        }
    }

    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync()!;
            return true;
        }
        finally
        {
            await _transaction?.DisposeAsync()!;
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
}
```

### 6. Cách dùng trong CQRS Handler
```csharp
// Ví dụ: CreatePlantCommandHandler.cs
public class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePlantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePlantCommand request, CancellationToken cancellationToken)
    {
        var plant = _mapper.Map<Plant>(request);
        plant.Slug = SlugGenerator.Generate(plant.Name);

        await _unitOfWork.Plants.AddAsync(plant);
        await _unitOfWork.SaveChangesAsync();

        return plant.Id;
    }
}

// Ví dụ: CreateOrderCommandHandler với transaction
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITelegramService _telegramService;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, ITelegramService telegramService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _telegramService = telegramService;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // 1. Upsert customer by phone
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Phone == request.Phone);
            if (customer == null)
            {
                customer = new Customer
                {
                    Name = request.CustomerName,
                    Phone = request.Phone,
                    Email = request.Email,
                    CustomerType = CustomerTypeEnum.Regular
                };
                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                customer.Name = request.CustomerName;
                customer.Email = request.Email;
                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.SaveChangesAsync();
            }

            // 2. Create order
            var order = new Order
            {
                OrderCode = GenerateOrderCode(),
                PlantId = request.PlantId,
                CustomerId = customer.Id,
                DepositAmount = request.DepositAmount,
                Status = OrderStatusEnum.Pending
            };
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // 3. Update plant status to Reserved
            var plant = await _unitOfWork.Plants.GetByIdAsync(request.PlantId);
            if (plant != null)
            {
                plant.Status = PlantStatusEnum.Reserved;
                _unitOfWork.Plants.Update(plant);
                await _unitOfWork.SaveChangesAsync();
            }

            // 4. Send Telegram notification
            await _telegramService.SendOrderNotificationAsync(order, customer, plant);

            // Commit transaction
            await _unitOfWork.CommitTransactionAsync();

            return order.OrderCode;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

// Ví dụ: Query handler
public class GetPlantsQueryHandler : IRequestHandler<GetPlantsQuery, IEnumerable<PlantResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPlantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlantResponseDto>> Handle(GetPlantsQuery request, CancellationToken cancellationToken)
    {
        var plants = await _unitOfWork.Plants.FindAsync(p => p.Status == PlantStatusEnum.Available);
        return _mapper.Map<IEnumerable<PlantResponseDto>>(plants);
    }
}
```

### 7. Đăng ký DI trong Program.cs
```csharp
// BanCayCanh.API/Program.cs
services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
    )
);
```

## Key SOLID Principles Applied

| Principle | Implementation |
|---|---|
| **S**ingle Responsibility | GenericRepository chỉ quản lý data access; Handler quản lý business logic |
| **O**pen/Closed | GenericRepository<T> có thể sử dụng cho bất kỳ entity nào mà không sửa code |
| **L**iskov Substitution | IRepository<T> có thể thay thế bằng bất kỳ implementation nào |
| **I**nterface Segregation | IRepository<T> tập trung vào CRUD; IUnitOfWork quản lý orchestration |
| **D**ependency Inversion | Handler depend on IUnitOfWork abstraction, không depend on concrete GenericRepository |

## Testing Strategy
- **Unit Tests**: Repository logic, business rules, value objects
- **Integration Tests**: CQRS handlers with in-memory EF Core DbContext
- **API Tests**: Controller endpoints with WebApplicationFactory

## Next Phase
After backend setup, scaffold Angular apps with:
- `/angular-architect` skill for customer-app
- `/angular-architect` skill for admin-app
