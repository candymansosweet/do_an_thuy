using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common.Mapping
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(GetType(), typeof(T)).ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) =>
                {
                    return srcMember != null;
                });
            });
        }
    }
}
