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
using ImageSlider.Models;

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



        public async Task<int> InsertIMGURL(string userid, string useralias, ImageModel imgmod)
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

                            // Input Parameters
                            sql_cmnd.Parameters.AddWithValue("@UserID", userid);
                            sql_cmnd.Parameters.AddWithValue("@UserAlias", (object)useralias ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Location", (object)imgmod.Image_Location ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Location_Orig", (object)imgmod.Image_Location_Orig ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Location_Small", (object)imgmod.Image_Location_Small ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Comment", (object)imgmod.Image_Comment ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Description", (object)imgmod.Image_Description ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Date", (object)imgmod.Image_Date ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Type", (object)imgmod.Image_Type ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Rotation", (object)imgmod.Image_Rotation ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Category_ID", (object)imgmod.Image_Category_ID ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Category", (object)imgmod.Image_Category ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Album_ID", (object)imgmod.Image_Album_ID ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Album_Name", (object)imgmod.Image_Album_Name ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Reference", (object)imgmod.Image_Reference ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@ProfileCover", (object)imgmod.ProfileCover ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Random", (object)imgmod.Random ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Showcase", (object)imgmod.Showcase ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@MediaPacketID", (object)imgmod.MediaPacketID ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Media_Id", (object)imgmod.Image_Media_Id ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Media_Name", (object)imgmod.Image_Media_Name ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Size", (object)imgmod.Image_Size ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Width", (object)imgmod.Image_Width ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Height", (object)imgmod.Image_Height ?? DBNull.Value);
                            sql_cmnd.Parameters.AddWithValue("@Image_Dimentions", (object)imgmod.Image_Dimentions ?? DBNull.Value);



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


        
    }
}
