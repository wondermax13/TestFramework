# TestFramework
Abstract generic exentendable class for efficient CRUD API tests implemented using reflection.

Sample usage

class DomainSpecificSystem : RemoteTestSystem<EntityType>
{
  //Domain specific client states
  
  protected override async Task<bool> HandleEntityCleanup(
            KeyValuePair<EntityType, string> createdEntity)
  {
      //Provide domain specific logic here
  }
  
}
