using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageSlider.Services
{
    public class DataAccess
    {
        private readonly string _cs;

        public DataAccess()
        {
           // private readonly string _cs = ConfigurationManager.ConnectionStrings["ImageSliderDB"]?.ConnectionString
           // ?? throw new InvalidOperationException("Connection string 'ImageSliderDB' not found in configuration.");



            // Read from App.config <connectionStrings>
            var cs = ConfigurationManager.ConnectionStrings["ImageSliderDB"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("Connection string 'ImageSliderDB' not found in configuration.");

            _cs = cs;
        }


        // ---------- Helpers ----------
        
    

        public async Task<int> InsertIMGURL(string userid, string useralias, string imageurl)
        {
            int newId = 0;

            var connDB = _cs;
            if ((userid.Length > 0) && (useralias.Length > 0))
            {
                try
                {
                    using (Microsoft.Data.SqlClient.SqlConnection sqlCon = new Microsoft.Data.SqlClient.SqlConnection(connDB))
                    {
                        await sqlCon.OpenAsync().ConfigureAwait(false);

                        using (Microsoft.Data.SqlClient.SqlCommand sql_cmnd = new Microsoft.Data.SqlClient.SqlCommand("spInsertVBImageURL", sqlCon))
                        {
                            sql_cmnd.CommandType = CommandType.StoredProcedure;

                            // ✅ Input Parameters - Fixed to use correct parameter names
                            sql_cmnd.Parameters.AddWithValue("@UserID", userid);
                            sql_cmnd.Parameters.AddWithValue("@UserAlias", useralias);
                            sql_cmnd.Parameters.AddWithValue("@Image_Location", imageurl);
                            sql_cmnd.Parameters.AddWithValue("@Image_Location_Orig", imageurl);
                            sql_cmnd.Parameters.AddWithValue("@Image_Location_Small", "pic01_sm.jpg");

                            // ✅ OUTPUT Parameter
                            Microsoft.Data.SqlClient.SqlParameter outputIdParam = new Microsoft.Data.SqlClient.SqlParameter
                            {
                                ParameterName = "@VB_Image_Key_Out",
                                SqlDbType = SqlDbType.Int,
                                Direction = ParameterDirection.Output
                            };

                            sql_cmnd.Parameters.Add(outputIdParam);

                            // Execute async
                            await sql_cmnd.ExecuteNonQueryAsync().ConfigureAwait(false);

                            // ✅ Read OUTPUT Value
                            if (outputIdParam.Value != DBNull.Value)
                            {
                                newId = Convert.ToInt32(outputIdParam.Value);
                            }
                        }
                    }
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    string logWriterErr = ex.Message;
                    // TODO: log properly if needed
                    throw; // Consider re-throwing or logging properly
                }
            }

            return newId;
        }

        //public List<ModelKeyWord> GetKeyWordByQID(string QID)
        //{
        //    var qDataList = new List<ModelKeyWord>();
        //    SqlDataReader rdr = null;
        //    var connDB = _cs;

        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connDB))
        //        {
        //            sqlCon.Open();

        //            using (SqlCommand sql_cmnd = new SqlCommand("spGetKeywordByQID", sqlCon))
        //            {
        //                sql_cmnd.CommandType = CommandType.StoredProcedure;

        //                // Correct parameter usage
        //                sql_cmnd.Parameters.Add("@QID", SqlDbType.Int).Value = Convert.ToInt32(QID);

        //                rdr = sql_cmnd.ExecuteReader();

        //                while (rdr.Read())
        //                {
        //                    // ✅ NEW instance each loop
        //                    ModelKeyWord qData = new ModelKeyWord();

        //                    // Use column indexes or names depending on your resultset
        //                    qData.Category = rdr[0].ToString();
        //                    qData.QuestionID = rdr[1].ToString();
        //                    qData.Keyword = rdr[2].ToString();

        //                    qDataList.Add(qData); // ✅ Add unique object each time
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        // TODO: Log exception as needed
        //        // logWriter = new("Error on GetKeyWordByQID: " + ex.Message, Globals.CurrentUser.Username);
        //    }
        //    finally
        //    {
        //        rdr?.Close();
        //    }

        //    return qDataList;
        //}

        ///// <summary>
        ///// Get all test results for a given user id.
        ///// </summary>
        //public List<ModelQuestionsAnswersbyUserId> Get_TestResultbyUserIdList(string uid)
        //{
        //    if (string.IsNullOrWhiteSpace(uid))
        //        throw new ArgumentException("User ID is required.", nameof(uid));

        //    if (!int.TryParse(uid, out var userId))
        //        throw new ArgumentException("User ID must be a valid integer.", nameof(uid));

        //    var results = new List<ModelQuestionsAnswersbyUserId>();
        //    var connDB = _cs;

        //    try
        //    {
        //        using (var sqlCon = new SqlConnection(connDB))
        //        using (var sqlCmd = new SqlCommand("Get_TestResultsByUserId", sqlCon))
        //        {
        //            sqlCmd.CommandType = CommandType.StoredProcedure;
        //            sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

        //            sqlCon.Open();

        //            using (var rdr = sqlCmd.ExecuteReader())
        //            {
        //                // These must match the columns returned by Get_TestResultsByUserId
        //                int oAnswerResponseId = rdr.GetOrdinal("AnswerResponseId");
        //                int oUserId = rdr.GetOrdinal("UserId");
        //                int oUserAlias = rdr.GetOrdinal("UserAlias");
        //                int oQuestionId = rdr.GetOrdinal("QuestionId");
        //                int oQuestionShort = rdr.GetOrdinal("QuestionShort");
        //                int oAnswerShort = rdr.GetOrdinal("AnswerShort");
        //                int oScore = rdr.GetOrdinal("Score");

        //                while (rdr.Read())
        //                {
        //                    var item = new ModelQuestionsAnswersbyUserId
        //                    {
        //                        AnswerResponseId = !rdr.IsDBNull(oAnswerResponseId) ? Convert.ToInt32(rdr[oAnswerResponseId]) : 0,
        //                        UserID = !rdr.IsDBNull(oUserId) ? Convert.ToInt32(rdr[oUserId]) : 0,
        //                        UserAlias = !rdr.IsDBNull(oUserAlias) ? rdr[oUserAlias].ToString() : null,
        //                        QuestionID = !rdr.IsDBNull(oQuestionId) ? Convert.ToInt32(rdr[oQuestionId]) : 0,
        //                        QuestionShort = !rdr.IsDBNull(oQuestionShort) ? rdr[oQuestionShort].ToString() : null,
        //                        AnswerShort = !rdr.IsDBNull(oAnswerShort) ? rdr[oAnswerShort].ToString() : null,
        //                        Score = !rdr.IsDBNull(oScore) ? rdr[oScore].ToString() : null
        //                    };

        //                    results.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        string logWriterErr = ex.Message;
        //        // TODO: log or rethrow as needed
        //    }

        //    return results;
        //}

        ///// <summary>
        ///// Get a single test result by TestId (AnswerResponse.Id).
        ///// Maps the fields returned by [dbo].[Get_TestResultbyTestId].
        ///// </summary>
        //public ModelQuestionsAnswersbyTestId Get_TestResultbyTestId(string testId)
        //{
        //    var connDB = _cs;

        //    try
        //    {
        //        using (var sqlCon = new SqlConnection(connDB))
        //        using (var sqlCmd = new SqlCommand("Get_TestResultbyTestId", sqlCon))
        //        {
        //            sqlCmd.CommandType = CommandType.StoredProcedure;
        //            sqlCmd.Parameters.Add("@TestId", SqlDbType.Int).Value = testId;

        //            sqlCon.Open();

        //            using (var rdr = sqlCmd.ExecuteReader(CommandBehavior.SingleRow))
        //            {
        //                if (!rdr.Read())
        //                    return null;

        //                // Match the stored proc columns exactly:
        //                //  ar.Id            AS AnswerResponseId,
        //                //  ar.UserId,
        //                //  ui.UserAlias,
        //                //  ar.QuestionId,
        //                //  q.Question       AS QuestionFull,
        //                //  ar.Answer        AS AnswerUser,
        //                //  ar.Answer        AS AnswerFull,
        //                //  ar.Score,
        //                //  ar.ScoreDescription

        //                int oAnswerResponseId = rdr.GetOrdinal("AnswerResponseId");
        //                int oUserId = rdr.GetOrdinal("UserId");
        //                int oUserAlias = rdr.GetOrdinal("UserAlias");
        //                int oQuestionId = rdr.GetOrdinal("QuestionId");
        //                int oQuestionFull = rdr.GetOrdinal("QuestionFull");
        //                int oAnswerUser = rdr.GetOrdinal("AnswerUser");
        //                int oAnswerFull = rdr.GetOrdinal("AnswerFull");
        //                int oScore = rdr.GetOrdinal("Score");
        //                int oScoreDescription = rdr.GetOrdinal("ScoreDescription");

        //                var item = new ModelQuestionsAnswersbyTestId
        //                {
        //                    AnswerResponseId = !rdr.IsDBNull(oAnswerResponseId) ? Convert.ToInt32(rdr[oAnswerResponseId]) : 0,
        //                    UserID = !rdr.IsDBNull(oUserId) ? Convert.ToInt32(rdr[oUserId]) : 0,

        //                    // Model has UserAlias as int, but SP returns a string.
        //                    // Try to parse; fall back to 0 if not numeric.
        //                    UserAlias = !rdr.IsDBNull(oUserAlias)
        //                                     ? (int.TryParse(rdr[oUserAlias].ToString(), out var ua) ? ua : 0)
        //                                     : 0,

        //                    QuestionID = !rdr.IsDBNull(oQuestionId) ? rdr[oQuestionId].ToString() : null,
        //                    QuestionFull = !rdr.IsDBNull(oQuestionFull) ? rdr[oQuestionFull].ToString() : null,
        //                    AnswerUser = !rdr.IsDBNull(oAnswerUser) ? rdr[oAnswerUser].ToString() : null,
        //                    AnswerFull = !rdr.IsDBNull(oAnswerFull) ? rdr[oAnswerFull].ToString() : null,

        //                    // Model Score is int; SP Score may be numeric or string -> Convert.ToInt32 with null guard
        //                    Score = !rdr.IsDBNull(oScore) && int.TryParse(rdr[oScore].ToString(), out var sc) ? sc : 0,
        //                    ScoreDesc = !rdr.IsDBNull(oScoreDescription) ? rdr[oScoreDescription].ToString() : null
        //                };



        //                return item;
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        string logWriterErr = ex.Message;
        //        return null;
        //    }
        //}
        //public void InsertKeyWord(string cat, string questionid, string keyword)
        //{

        //    SqlConnection conn = null;
        //    SqlDataReader rdr = null;

        //    var connDB = ConfigurationManager.ConnectionStrings["TestingDB"].ConnectionString;

        //    try
        //    {
        //        using (SqlConnection sqlCon = new SqlConnection(connDB))
        //        {
        //            sqlCon.Open();

        //            using (SqlCommand sql_cmnd = new SqlCommand("spInsertKeyWords", sqlCon))
        //            {
        //                sql_cmnd.CommandType = CommandType.StoredProcedure;

        //                sql_cmnd.Parameters.AddWithValue("@category", SqlDbType.NVarChar).Value = cat;
        //                sql_cmnd.Parameters.AddWithValue("@questionid", SqlDbType.NVarChar).Value = questionid;
        //                sql_cmnd.Parameters.AddWithValue("@keyword", SqlDbType.NVarChar).Value = keyword;

        //                sql_cmnd.ExecuteNonQuery();
        //                sqlCon.Close();

        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {

        //        string logWriterErr = ex.Message;
        //    }
        //    finally
        //    {
        //        conn?.Close();
        //        rdr?.Close();
        //    }
        //}

        //public async Task InsertKeyWordAsync(string cat, string questionid, string keyword)
        //{
        //    var connDB = _cs;

        //    try
        //    {
        //        using (var sqlCon = new SqlConnection(connDB))
        //        {
        //            await sqlCon.OpenAsync().ConfigureAwait(false);

        //            using (var sql_cmnd = new SqlCommand("spInsertKeyWords", sqlCon))
        //            {
        //                sql_cmnd.CommandType = CommandType.StoredProcedure;

        //                // Note: you probably meant to pass the values directly, not SqlDbType
        //                sql_cmnd.Parameters.AddWithValue("@category", cat);
        //                sql_cmnd.Parameters.AddWithValue("@questionid", questionid);
        //                sql_cmnd.Parameters.AddWithValue("@keyword", keyword);

        //                await sql_cmnd.ExecuteNonQueryAsync().ConfigureAwait(false);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        string logWriterErr = ex.Message;
        //        // TODO: log properly if needed
        //    }


        //public async Task<ModelGradeReturn> InsertAnswerAsync(string trackid, string answer)
        //{
        //    var connDB = _cs;

        //    // Initialize result
        //    var gradeResult = new ModelGradeReturn
        //    {
        //        QuestionID = trackid,
        //        KeywordList = new List<string>(),
        //        NumKeyWords = 0,
        //        Matches = 0,
        //        Grade = 0m
        //    };

        //    try
        //    {
        //        // 1) Save the answer (async)
        //        using (var sqlCon = new SqlConnection(connDB))
        //        {
        //            await sqlCon.OpenAsync().ConfigureAwait(false);

        //            using (var sql_cmnd = new SqlCommand("spInsertAnswerResponse", sqlCon))
        //            {
        //                sql_cmnd.CommandType = CommandType.StoredProcedure;

        //                sql_cmnd.Parameters.AddWithValue("@trackid", trackid);
        //                sql_cmnd.Parameters.AddWithValue("@answer", answer);

        //                await sql_cmnd.ExecuteNonQueryAsync().ConfigureAwait(false);
        //            }
        //        }

        //        // 2) Get the list of keywords for this question id (sync for now)
        //        var keywordModels = GetKeyWordByQID(trackid);



        //        if (keywordModels != null && keywordModels.Count > 0)
        //        {
        //            var answerLower = answer.ToLowerInvariant();
        //            int totalKeywords = keywordModels.Count;

        //            // 2a) Build the full list of KeywordUnit from all keywords
        //            var allKeywordUnits =
        //                keywordModels
        //                    .Where(k => !string.IsNullOrWhiteSpace(k.Keyword))
        //                    .Select(k => new KeywordUnit
        //                    {
        //                        QuestionId = k.QuestionID,
        //                        Keyword = k.Keyword
        //                    })
        //                    .ToList();
        //            var allKeywords =
        //                keywordModels
        //                    .Where(k => !string.IsNullOrWhiteSpace(k.Keyword))
        //                    .Select(k => k.Keyword)
        //                    .ToList();

        //            // 2b) Subset: matching keywords (case‑insensitive substring match)
        //            var matchingKeywordUnits =
        //                allKeywordUnits
        //                    .Where(ku => answerLower.Contains(ku.Keyword.ToLowerInvariant()))
        //                    .ToList();

        //            int matchCount = matchingKeywordUnits.Count;

        //            // 2c) Calculate percentage grade
        //            decimal percentGrade = 0m;
        //            if (totalKeywords > 0)
        //            {
        //                percentGrade = Math.Round((decimal)matchCount / totalKeywords * 100m);
        //            }

        //            // 2d) Populate GradeReturn with the FULL list of keywords
        //            gradeResult.KeywordList = allKeywords;
        //            gradeResult.NumKeyWords = totalKeywords;
        //            gradeResult.Matches = matchCount;
        //            gradeResult.Grade = percentGrade;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        string logWriterErr = ex.Message;
        //        // TODO: log properly if needed
        //    }

        //    return gradeResult;
        //}

        //public async Task<ModelGradeReturn> InsertAnswerScoreAsync(string trackid, string answer, string userid, string category)
        //{
        //    var connDB = _cs;
        //    string scoredescription = string.Empty;
        //    // Initialize result
        //    var gradeResult = new ModelGradeReturn
        //    {
        //        QuestionID = trackid,
        //        KeywordList = new List<string>(),
        //        NumKeyWords = 0,
        //        Matches = 0,
        //        Grade = 0m
        //    };

        //    try
        //    {


        //        // 2) Get the list of keywords for this question id (sync for now)
        //        var keywordModels = GetKeyWordByQID(trackid);



        //        if (keywordModels != null && keywordModels.Count > 0)
        //        {
        //            var answerLower = answer.ToLowerInvariant();
        //            int totalKeywords = keywordModels.Count;

        //            // 2a) Build the full list of KeywordUnit from all keywords
        //            var allKeywordUnits =
        //                keywordModels
        //                    .Where(k => !string.IsNullOrWhiteSpace(k.Keyword))
        //                    .Select(k => new KeywordUnit
        //                    {
        //                        QuestionId = k.QuestionID,
        //                        Keyword = k.Keyword
        //                    })
        //                    .ToList();
        //            var allKeywords =
        //                keywordModels
        //                    .Where(k => !string.IsNullOrWhiteSpace(k.Keyword))
        //                    .Select(k => k.Keyword)
        //                    .ToList();

        //            // 2b) Subset: matching keywords (case‑insensitive substring match)
        //            var matchingKeywordUnits =
        //                allKeywordUnits
        //                    .Where(ku => answerLower.Contains(ku.Keyword.ToLowerInvariant()))
        //                    .ToList();

        //            int matchCount = matchingKeywordUnits.Count;

        //            // 2c) Calculate percentage grade
        //            decimal percentGrade = 0m;
        //            if (totalKeywords > 0)
        //            {
        //                percentGrade = Math.Round((decimal)matchCount / totalKeywords * 100m);
        //            }

        //            // 2d) Populate GradeReturn with the FULL list of keywords
        //            gradeResult.KeywordList = allKeywords;
        //            gradeResult.NumKeyWords = totalKeywords;
        //            gradeResult.Matches = matchCount;
        //            gradeResult.Grade = percentGrade;

        //            scoredescription = percentGrade.ToString() + " Keyword List " + " Keyword Match Count " + matchCount.ToString();
        //        }

        //        // 1) Save the answer (async)
        //        using (var sqlCon = new SqlConnection(connDB))
        //        {
        //            await sqlCon.OpenAsync().ConfigureAwait(false);

        //            using (var sql_cmnd = new SqlCommand("spInsertAnswersScore", sqlCon))
        //            {
        //                sql_cmnd.CommandType = CommandType.StoredProcedure;


        //                sql_cmnd.Parameters.AddWithValue("@userid", userid);
        //                sql_cmnd.Parameters.AddWithValue("@questionid", trackid);
        //                sql_cmnd.Parameters.AddWithValue("@category", category);
        //                sql_cmnd.Parameters.AddWithValue("@answer", answer);
        //                sql_cmnd.Parameters.AddWithValue("@score", gradeResult.Grade);
        //                sql_cmnd.Parameters.AddWithValue("@scoredescription", scoredescription);

        //                await sql_cmnd.ExecuteNonQueryAsync().ConfigureAwait(false);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        string logWriterErr = ex.Message;
        //        // TODO: log properly if needed
        //    }

        //    return gradeResult;
        //}


        

        
    }
}
