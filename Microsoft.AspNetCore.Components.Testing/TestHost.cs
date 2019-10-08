using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Testing
{
    public class TestHost
    {
        private readonly ServiceCollection _serviceCollection = new ServiceCollection();
        private readonly Lazy<TestRenderer> _renderer;

        public TestHost()
        {
            _renderer = new Lazy<TestRenderer>(() =>
            {
                var serviceProvider = _serviceCollection.BuildServiceProvider();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>() ?? new NullLoggerFactory();
                return new TestRenderer(serviceProvider, loggerFactory);
            });
        }

        //public void WaitForNextRender(Action trigger)
        //{
        //    var task = Renderer.NextRender;
        //    trigger();
        //    task.Wait(millisecondsTimeout: 1000);

        //    if (!task.IsCompleted)
        //    {
        //        throw new TimeoutException("No render occurred within the timeout period.");
        //    }
        //}

        public RenderedComponent<TComponent> AddComponent<TComponent>() where TComponent: IComponent
        {
            var result = new RenderedComponent<TComponent>(Renderer);
            result.SetParametersAndRender(ParameterView.Empty);
            return result;
        }

        private TestRenderer Renderer => _renderer.Value;
    }
}
