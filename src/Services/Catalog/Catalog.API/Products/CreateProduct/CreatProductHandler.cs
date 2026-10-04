namespace Catalog.API.Products.CreateProduct
{
    public record class CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record class CreateProductResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreatProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Handle creating a product

            //remove the manualy validator, use behavior pipeline instead

            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(x=>x.ErrorMessage).ToList();
            //if(errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            //create product entity
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            //save product to db
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return the created product
            return new CreateProductResult(product.Id);
        }
    }
}
