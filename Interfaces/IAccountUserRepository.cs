using BankAppWebApi.Models;

namespace BankAppWebApi.Interfaces
{
    public interface IAccountUserRepository
    {
        ICollection<AccountUser> GetAccountUsers();
        AccountUser GetAccountUserById(string Id);
        AccountUser GetAccountUserByAccountNumber(string AccountNumber);
        bool AccountUserExist(string AccountNumber);
        //UserData GetPokemonTrimToUpper(PokemonDto pokemonCreate);
        //bool CreatePokemon(int ownerId, int catId, UserData pokemonMap);
        //bool UpdatePokemon(int ownerId, int catId, UserData pokemonMap);
        bool Save();
        bool CreateAccountUser(AccountUser accountUser);
        AccountUser AddUser(AccountUser accountUser);
    }
}
