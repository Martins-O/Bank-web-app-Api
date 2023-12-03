using BankAppWebApi.Models;

namespace BankAppWebApi.Interfaces
{
    public interface IUserDataRepository
    {
        ICollection<UserData> GetUserDatas();
        UserData GetUserDataById(string Id);
        UserData GetUserDataByEmailAddress(string Email);
        bool UserDataExist(string EmailAddress);
        //UserData GetPokemonTrimToUpper(PokemonDto pokemonCreate);
        //bool CreatePokemon(int ownerId, int catId, UserData pokemonMap);
        //bool UpdatePokemon(int ownerId, int catId, UserData pokemonMap);
        bool Save();
        bool CreateUserData(UserData userData);
        UserData AddUser(UserData user);
    }
}
