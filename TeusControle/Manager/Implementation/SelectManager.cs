using Core.Shared.Models.Enums;
using Manager.Interfaces;
using Manager.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Manager.Implementation
{
    public class SelectManager : ISelectManager
    {
        private readonly ILogger<EntryManager> logger;

        public SelectManager(
            ILogger<EntryManager> logger
        )
        {
            this.logger = logger;
           
        }

        /// <summary>
        /// Busca tipos de perfil de usuários
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> getUserTypeSelect()
        {
            try
            {
                List<object> result = new List<object>();
                foreach (ProfileTypesEnum enumerator in EnumExtension.GetValues<ProfileTypesEnum>())
                {
                    result.Add(new
                    {
                        Value = enumerator,
                        Description = EnumExtension.GetDescription(enumerator)
                    });
                }
                
                return result;
            } catch(Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR ITENS DE SELEÇÃO PARA PROFILETYPES", ex);
            }
            return null;
        }

        /// <summary>
        /// Busca status para entrada de produtos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> getEntryStatusSelect()
        {
            try
            {
                List<object> result = new List<object>();
                foreach (EntryStatusEnum enumerator in EnumExtension.GetValues<EntryStatusEnum>())
                {
                    result.Add(new
                    {
                        Value = enumerator,
                        Description = EnumExtension.GetDescription(enumerator)
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR ITENS DE SELEÇÃO PARA ENTRYSTATUS", ex);
            }
            return null;
        }

        /// <summary>
        /// Busca status para vendas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetSaleStatusSelect()
        {
            try
            {
                List<object> result = new List<object>();
                foreach (SaleStatusEnum enumerator in EnumExtension.GetValues<SaleStatusEnum>())
                {
                    result.Add(new
                    {
                        Value = enumerator,
                        Description = EnumExtension.GetDescription(enumerator)
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR ITENS DE SELEÇÃO PARA SALESTATUS", ex);
            }
            return null;
        }
    }
}
