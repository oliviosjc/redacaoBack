using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Domain.Enums
{
    public class RedacaoEnums
    {
        public class StatusRedacaoEnum
        {
            public static Guid INICIADA = new Guid("519A585F-7C69-4E21-A18D-840CCDB197D1");

            public static Guid EM_ANALISE = new Guid("E25091EA-BEF3-4705-9CB4-EA869C6C5523");

            public static Guid FINALIZADA = new Guid("7CDF8A2F-DB5E-4435-ACB9-BB4E2714FE00");
        }
    }
}
