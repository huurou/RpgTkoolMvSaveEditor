using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model.Queries.Common;

public interface IQueryHandler<TQuery, T> where TQuery : IQuery
{
    Task<Result<T>> HandleAsync(TQuery query);
}
