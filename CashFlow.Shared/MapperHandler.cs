using AutoMapper;
using System.Linq.Expressions;

namespace CashFlow.Shared
{
    public class MapperHandler
    {
        private static IMapper? _mapper;

        public static void Initialize(List<Profile>? profiles = null)
        {
            if (_mapper == null)
            {
                MapperConfiguration configuration = new(c => {
                    if (profiles != null && profiles.Any())
                    {
                        foreach (var profile in profiles)
                        {
                            c.AddProfile(profile);
                        }
                    }
                });
                configuration.AssertConfigurationIsValid();
                configuration.CompileMappings();

                _mapper = configuration.CreateMapper();
            }
        }

        public static IMapper GetMapper()
        {
            if (_mapper == null)
            {
                throw new NullReferenceException();
            }

            return _mapper;
        }

        public static TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
        {
            return GetMapper().Map(source, opts);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return GetMapper().Map(source, opts);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return GetMapper().Map(source, destination, opts);
        }

        public static object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            return GetMapper().Map(source, sourceType, destinationType, opts);
        }

        public static object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            return GetMapper().Map(source, destination, sourceType, destinationType, opts);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object? parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return GetMapper().ProjectTo(source, parameters, membersToExpand);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return GetMapper().ProjectTo<TDestination>(source, parameters, membersToExpand);
        }

        public static IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object>? parameters = null, params string[] membersToExpand)
        {
            return GetMapper().ProjectTo(source, destinationType, parameters, membersToExpand);
        }

        public static TDestination Map<TDestination>(object source)
        {
            return GetMapper().Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return GetMapper().Map<TSource, TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return GetMapper().Map(source, destination);
        }

        public static object Map(object source, Type sourceType, Type destinationType)
        {
            return GetMapper().Map(source, sourceType, destinationType);
        }

        public static object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return GetMapper().Map(source, destination, sourceType, destinationType);
        }
    }
}
