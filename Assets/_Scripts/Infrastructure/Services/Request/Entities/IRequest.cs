using Cysharp.Threading.Tasks;
using System;

namespace Infrastructure.Services.Request
{
    public interface IRequest<T>
    {
        public UniTask<T> SendAsync();
    }
}