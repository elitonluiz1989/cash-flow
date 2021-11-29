using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CashFlow.Infra.Data.Mapping
{
    public class AppMapper
    {
        private static IMapper _mapper;

        public static void Initialize()
        {
            if (_mapper == null)
            {
                MapperConfiguration configuration = new(c => {});
                configuration.AssertConfigurationIsValid();
                configuration.CompileMappings();

                _mapper = configuration.CreateMapper();
            }
        }

        public static IMapper GetMapper()
        {
            return _mapper;
        }

        public static TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
        {
            return _mapper.Map(source, opts);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return _mapper.Map(source, opts);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return _mapper.Map(source, destination, opts);
        }

        public static object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            return _mapper.Map(source, sourceType, destinationType, opts);
        }

        public static object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            return _mapper.Map(source, destination, sourceType, destinationType, opts);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object? parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return _mapper.ProjectTo(source, parameters, membersToExpand);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return _mapper.ProjectTo<TDestination>(source, parameters, membersToExpand);
        }

        public static IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object>? parameters = null, params string[] membersToExpand)
        {
            return _mapper.ProjectTo(source, destinationType, parameters, membersToExpand);
        }

        public static TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        public static object Map(object source, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, sourceType, destinationType);
        }

        public static object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, destination, sourceType, destinationType);
        }
    }
}
