using System;
using System.Diagnostics;
using System.Web.Mvc;
using StructureMap;
using System.Web.Routing;

namespace HotelOrTaxi.StructureMap
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if ((requestContext == null) || (controllerType == null))
                return null;

            return (Controller)ObjectFactory.GetInstance(controllerType);
        }
    }
}