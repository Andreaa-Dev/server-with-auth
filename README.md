Notes

### Packages

1. Npgsql
2. EFCore.NamingConventions
3. Npgsql.EntityFrameworkCore.PostgreSQL
4. Swashbuckle.AspNetCore.Filters

### Notes

1. connection string in appsettings
2. DTO: Data Transfer Objects: transfer data between different layers of an application, particularly between the client and server. DTO - response
3. Repo
4. Interface in service
5. Mapper:
6. IMapper: AutoMapper instance used to map between Category entities and Category DTOs.
7. The Task represents the ongoing work and will complete when the operation is done.
8. mapper.Map(updateDto, foundCategory) vs mapper.Map<Category, CategoryReadDto>(foundCategory)

- update an existing object (foundCategory) with new data from another object (updateDto)
- create new object (CategoryReadDto) from existing object (found category)

9. folder division by 2 ways: feature (category, product, user) and layer (controller, service)

10. appsettings.json - hide important data. add to gitignore
11. admin: 3 ways: change in database, create admin and make other become admin
12. error handler in service.

- 404 NotFound: NOTE: for demo product id, it has to have Guid type (dont remove id ONLY change value)
- 410 UnAuthorized - users do not log in
- 500 Internal server error - send request with wrong methods/endpoint
- 400 Bad request (client error response) - create usr with same email

### Migration

- dotnet ef migrations add InitialCreate
- dotnet ef database update

* remember: dotnet add package Microsoft.EntityFrameworkCore.Design (same version with .NET)

### TO DO

- annotation:
  entity
  [Key]
  public Guid Id { get;set; }

  [Required, StringLength(100)]
  public string Name { get; set; }

  [Required, ForeignKey("User")]
  public Guid UserId { get; set; }

unique? password?

DTO
[Required(ErrorMessage = "Password is required")]
[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
[DataType(DataType.Password)]
public string Password { get; set; }

- deploy docker
