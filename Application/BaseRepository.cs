using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BaseRepository<TEntity> : GenericRepository<TEntity> where TEntity : Domain.BaseModel
    {
        protected readonly IMapper  _mapper;
        public BaseRepository(ApplicationContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
