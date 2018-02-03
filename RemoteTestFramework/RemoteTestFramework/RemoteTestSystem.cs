using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace RemoteTestFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteTestSystem<T>
    {

        protected const string Default_Locale = "en-us";

        //Created Entities <Type, Id>
        protected List<KeyValuePair<T, string>> createdEntities;


        public RemoteTestSystem()
        {
            createdEntities = new List<KeyValuePair<T, string>>();
        }

        public async Task Test(string methodName)
        {
            try
            {

                MethodInfo method
                    = this.GetType().GetMethod(
                        methodName, BindingFlags.NonPublic | BindingFlags.Instance);

                await (Task)method.Invoke(this, null);

                bool cleanupSucceded = await this.CommonCleanUp();
                if (!cleanupSucceded)
                {
                    throw new CleanupFailureException("Cleanup failed to delete all entities");
                }
            }
            catch (KeyNotFoundException)
            {
                throw new NotSupportedException(" Test method does not exist: " + methodName);
            }
            catch (CleanupFailureException e)
            {
                throw e;
            }
            catch (System.Exception e)
            {
                string message = e.Message;

                bool cleanupSucceded = await this.CommonCleanUp();
                if (!cleanupSucceded)
                {
                    message = message + " and cleaup failed";
                }

                throw new InternalTestFailureException(e.Message);
            }
        }

        /// <summary>
        /// Common logic for cleanup of all the existing entities
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CommonCleanUp()
        {
            bool cleanupSucceded = true;
            for (int index = createdEntities.Count - 1; index > -1; index--)
            {
                bool cleanup = await this.HandleEntityCleanup(createdEntities[index]);
                cleanupSucceded = cleanupSucceded && cleanup;
            }

            return cleanupSucceded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createdEntity"></param>
#pragma warning disable CS1998
        protected virtual async Task<bool> HandleEntityCleanup(
            KeyValuePair<T, string> createdEntity)
        {
            return true;
        }
#pragma warning restore CS1998

    }
}