﻿using System;
using Glimpse.Agent.AspNet.Messages;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Agent.Inspectors
{
    // TODO: Is this what we want to name this type?
    public abstract class Tab : Inspector
    {
        private IAgentBroker _broker;
        public IAgentBroker Broker
        {
            get
            {
                if (_broker == null)
                {
                    var httpContextAccessor = new HttpContextAccessor();
                    _broker = httpContextAccessor.HttpContext.RequestServices.GetService<IAgentBroker>();
                }

                return _broker;
            }
            set { _broker = value; }
        }

        public override void Before(HttpContext context)
        {
            if (ExecuteWhen == Execute.BeforeResponse)
                Publish(context);
        }

        public override void After(HttpContext context)
        {
            if (ExecuteWhen == Execute.AfterResponse)
                Publish(context);
        }

        private void Publish(HttpContext context)
        {
            object data = null;
            try
            {
                data = GetData(context);
            }
            catch (Exception exception)
            {
                data = exception;
            }

            Broker.SendMessage(new TabMessage(Name, data));
        }

        public virtual Execute ExecuteWhen => Execute.AfterResponse;

        public abstract string Name { get; }

        // TODO: Is HttpContext the right type to pass in here?
        public abstract object GetData(HttpContext context);
    }
}