namespace Catalog.API.Products.GetProduct
{
    public record GetProductsQuery(int? PageNumer=1, int? PageSize=10): IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumer ?? 1, query.PageSize ?? 10, cancellationToken);
            return new GetProductResult(products);
        }
    }
}                               
