using System.Data;
using Domain;
using Infrastructure.Repository.Interfaces;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly IDbConnection? _mySql;

    public PaymentRepository(IConfiguration config)
    {
        _mySql = new MySqlConnection(config.GetConnectionString("mySqlGeneral"));
    }

    public async Task<IEnumerable<PaymentMethodsBigImage>> GetPaymentMethodsBigImages()
    {
        try
        {
            return await _mySql.QueryAsync<PaymentMethodsBigImage>(@"
                            SELECT internalId, nome, bigImage FROM `PayFlow`.`Payments.Methods`
                            WHERE status = 1");
        }
        catch (Exception)
        {
            throw new Exception("Error while fetching image data.");
        }
    }

    public async Task<IEnumerable<PaymentMethodsSmallImage>> GetPaymentMethodsSmallImages()
    {
        try
        {
            return await _mySql.QueryAsync<PaymentMethodsSmallImage>(@"
                                SELECT internalId, nome, smallImage FROM `PayFlow`.`Payments.Methods`
                                WHERE status = 1");
        }
        catch (Exception)
        {
            throw new Exception("Error while fetching image data.");
        }
    }

    public async Task<BankTransfer> GetBankTransfer()
    {
        try
        {
            return await _mySql.QueryFirstOrDefaultAsync<BankTransfer>(@"
                                SELECT * FROM `PayFlow`.`Configuration`");
        }
        catch (Exception)
        {
            throw new Exception("Error while fetching bank transfer settings.");
        }
    }
}

