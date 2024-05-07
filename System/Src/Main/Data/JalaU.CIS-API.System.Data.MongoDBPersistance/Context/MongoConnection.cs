//-----------------------------------------------------------------------
// <copyright file="MongoConnection.cs" company="Jala University">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MongoDB.Driver;

namespace JalaU.CIS_API.System.Data.MongoDBPersistance;

/// <summary>
/// Initializes a new instance of the <see cref="MongoDbContext"/> class.
/// </summary>
/// <param name="options">The options for configuring the context.</param>
public class MongoConnection
{
    private const string ConnectionString =
        "mongodb+srv://abbybaem:qY3oFbRro1zyxkdi@sabrozitoz.wjreqdl.mongodb.net/";

    private const string DatabaseName = "cis";

    /// <summary>
    /// Connects to a MongoDB collection of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects stored in the MongoDB collection.</typeparam>
    /// <param name="collection">The name of the MongoDB collection.</param>
    /// <returns>An instance of IMongoCollection. <T> representing the MongoDB collection.</returns>
    public static IMongoCollection<T> ConnectToMongo<T>(string collection)
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        return db.GetCollection<T>(collection);
    }
}
