using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Enums
{
    public class RedacaoEnums
    {
        public class StatusRedacaoEnum
        {
            public static Guid INICIADA = new Guid("F1D014F9-8A5F-4CC8-899B-61E117685B7B");

            public static Guid EM_ANALISE = new Guid("1108543E-B581-4302-9BA0-EABB4AC61183");

            public static Guid FINALIZADA = new Guid("3516919F-6B2D-4668-8838-51E7ED106866");
        }
    }
}
