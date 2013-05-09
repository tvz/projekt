/// <summary>
/// Klasa je nazvana Users zato što klasa User vec postoji
/// </summary>
public class Users
{
    public enum databaseColumnName { ID,
                                     username,
                                     password_hash,
                                     pass_salt,
                                     created_at,
                                     updated_at,
                                     role_id,
                                     paypal_id,
                                     email,
                                     status_id };
    private int id;
    public string username;
    public string password_hash;
    public string pass_salt;

    public Users()
    {
    }
}
