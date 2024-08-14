Notes

### Packages

1. Npgsql
2. EFCore.NamingConventions
3. Npgsql.EntityFrameworkCore.PostgreSQL

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

### Questions

- error handler -
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

- appsettings.json - hide important data

- deploy docker

- feature: category, product, user
- layer
