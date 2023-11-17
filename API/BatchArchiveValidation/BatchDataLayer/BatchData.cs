using BatchDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BatchDataLayer
{
    public class BatchData
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<BatchUnits> GetBatchUnits(string _connectionString)
        {
            List<BatchUnits> result = new List<BatchUnits>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("spLocal_PG_Batch_GetAvailableBatchUnits", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter Param_ErrorMessage = new SqlParameter("@op_vchErrorMessage", SqlDbType.VarChar, 1000);
                Param_ErrorMessage.Direction = ParameterDirection.Output;
                Param_ErrorMessage.Value = String.Empty;

                command.Parameters.Add(Param_ErrorMessage);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BatchUnits batchUnit = new BatchUnits();
                        if (reader["RcdIdx"] != DBNull.Value)
                        {
                            batchUnit.RcdIdx = Convert.ToInt32(reader["RcdIdx"]);
                        }
                        if (reader["ArchiveDatabase"] != DBNull.Value)
                        {
                            batchUnit.ArchiveDatabase = reader["ArchiveDatabase"].ToString();
                        }
                        if (reader["ArchiveTable"] != DBNull.Value)
                        {
                            batchUnit.ArchiveTable = reader["ArchiveTable"].ToString();
                        }
                        if (reader["Department"] != DBNull.Value)
                        {
                            batchUnit.Department = reader["Department"].ToString();
                        }
                        if (reader["Line"] != DBNull.Value)
                        {
                            batchUnit.Line = reader["Line"].ToString();
                        }
                        if (reader["Unit"] != DBNull.Value)
                        {
                            batchUnit.Unit = reader["Unit"].ToString();
                        }
                        if (reader["PUId"] != DBNull.Value)
                        {
                            batchUnit.PUId = Convert.ToInt32(reader["PUId"]);
                        }
                        result.Add(batchUnit);
                    }

                    reader.Close();
                }

                string message = command.Parameters["@op_vchErrorMessage"].Value.ToString();
                if (message != "Success")
                {
                    BatchUnits batchUnit = new BatchUnits();
                    batchUnit.Message = message;
                    result.Add(batchUnit);
                }
            }

            return result;
        }


        public List<Batch> GetBatch(string _connectionString, string pUIdList, string pDelimiter)
        {
            List<Batch> result = new List<Batch>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("spLocal_PG_Batch_GetAvailableBatches", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter Param_ErrorMessage = new SqlParameter("@op_vchErrorMessage", SqlDbType.VarChar, 1000);
                Param_ErrorMessage.Direction = ParameterDirection.Output;
                Param_ErrorMessage.Value = String.Empty;

                command.Parameters.Add(new SqlParameter("@p_vchDelimitedPUIdList", pUIdList));
                command.Parameters.Add(new SqlParameter("@p_vchDelimiter", pDelimiter));
                command.Parameters.Add(Param_ErrorMessage);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Batch batch = new Batch();

                        if (reader["RcdIdx"] != DBNull.Value)
                        {
                            batch.RcdIdx = Convert.ToInt32(reader["RcdIdx"]);
                        }
                        if (reader["BatchId"] != DBNull.Value)
                        {
                            batch.BatchId = reader["BatchId"].ToString();
                        }
                        if (reader["UniqueId"] != DBNull.Value)
                        {
                            batch.UniqueId = reader["UniqueId"].ToString();
                        }
                        if (reader["BatchName"] != DBNull.Value)
                        {
                            batch.BatchName = reader["BatchName"].ToString();
                        }
                        if (reader["PUId"] != DBNull.Value)
                        {
                            batch.PUId = Convert.ToInt32(reader["PUId"].ToString());
                        }
                        if (reader["UniqueIdPUId"] != DBNull.Value)
                        {
                            batch.UniqueIdPUId = reader["UniqueIdPUId"].ToString();
                        }
                        if (reader["PUDesc"] != DBNull.Value)
                        {
                            batch.PUDesc = reader["PUDesc"].ToString();
                        }
                        if (reader["StartTime"] != DBNull.Value)
                        {
                            batch.StartTime = Convert.ToDateTime(reader["StartTime"]);
                        }

                        result.Add(batch);
                    }

                    reader.Close();
                }
                var message = command.Parameters["@op_vchErrorMessage"].Value.ToString();

                if (message != "Success")
                {
                    Batch batch = new Batch();
                    batch.Message = message;
                    result.Add(batch);
                }
            }
            return result;
        }

        public GetArchiveData GetArchiveData(string _connectionString, string pDelimitedBatchList, string pDelimiter)
        {
            GetArchiveData getArchiveDataResult = new GetArchiveData();

            List<BatchSummary> batchSummaryResult = new List<BatchSummary>();
            List<ErrorMessages> errorMessagesResult = new List<ErrorMessages>();
            List<OrganizeCalculation> organizeCalculationResult = new List<OrganizeCalculation>();
            List<CreateConsumtion> createConsumtionResult = new List<CreateConsumtion>();
            List<TestConformance> testConformanceResult = new List<TestConformance>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("spLocal_PG_Batch_GetBatchArchiveData", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter Param_ErrorMessage = new SqlParameter("@op_vchErrorMessage", SqlDbType.VarChar, 1000);
                Param_ErrorMessage.Direction = ParameterDirection.Output;
                Param_ErrorMessage.Value = String.Empty;


                command.Parameters.Add(new SqlParameter("@p_vchDelimitedBatchList", pDelimitedBatchList));
                command.Parameters.Add(new SqlParameter("@p_vchDelimiter", pDelimiter));
                command.Parameters.Add(Param_ErrorMessage);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        BatchSummary batchSummary = new BatchSummary();
                        if (reader["Unit"] != DBNull.Value)
                        {
                            batchSummary.Unit = reader["Unit"].ToString();
                        }
                        if (reader["Batch"] != DBNull.Value)
                        {
                            batchSummary.Batch = reader["Batch"].ToString();
                        }
                        if (reader["RecordCount"] != DBNull.Value)
                        {
                            batchSummary.RecordCount = reader["RecordCount"].ToString();
                        }
                        if (reader["RecipeLayers"] != DBNull.Value)
                        {
                            batchSummary.RecipeLayers = reader["RecipeLayers"].ToString();
                        }
                        if (reader["BatchStartTime"] != DBNull.Value)
                        {
                            batchSummary.BatchStartTime = Convert.ToDateTime(reader["BatchStartTime"].ToString());
                        }
                        if (reader["BatchEndTime"] != DBNull.Value)
                        {
                            batchSummary.BatchEndTime = Convert.ToDateTime(reader["BatchEndTime"]);
                        }
                        if (reader["EndOfBatch"] != DBNull.Value)
                        {
                            batchSummary.EndOfBatch = Convert.ToBoolean(reader["EndOfBatch"]);
                        }
                        if (reader["Processed"] != DBNull.Value)
                        {
                            batchSummary.Processed = Convert.ToBoolean(reader["Processed"]);
                        }
                        if (reader["HeaderErrorSeverity"] != DBNull.Value)
                        {
                            batchSummary.HeaderErrorSeverity = reader["HeaderErrorSeverity"].ToString();
                        }
                        if (reader["S88ErrorSeverity"] != DBNull.Value)
                        {
                            batchSummary.S88ErrorSeverity = reader["S88ErrorSeverity"].ToString();
                        }
                        if (reader["EventCompErrorSeverity"] != DBNull.Value)
                        {
                            batchSummary.EventCompErrorSeverity = Convert.ToInt32(reader["EventCompErrorSeverity"]);
                        }
                        if (reader["TestConfErrorSeverity"] != DBNull.Value)
                        {
                            batchSummary.TestConfErrorSeverity = Convert.ToInt32(reader["TestConfErrorSeverity"]);
                        }
                        if (reader["UniqueId"] != DBNull.Value)
                        {
                            batchSummary.UniqueId = reader["UniqueId"].ToString();
                        }
                        batchSummaryResult.Add(batchSummary);
                    }

                    getArchiveDataResult.batchSummary = batchSummaryResult;

                    reader.NextResult();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            OrganizeCalculation organizeCalculation = new OrganizeCalculation();
                            if (reader["ParmType"] != DBNull.Value)
                            {
                                organizeCalculation.ParmType = reader["ParmType"].ToString();
                            }
                            if (reader["ProductCode"] != DBNull.Value)
                            {
                                organizeCalculation.ProductCode = reader["ProductCode"].ToString();
                            }
                            if (reader["BatchSize"] != DBNull.Value)
                            {
                                organizeCalculation.BatchSize = reader["BatchSize"].ToString();
                            }
                            if (reader["BatchEnd"] != DBNull.Value)
                            {
                                organizeCalculation.BatchEnd = reader["BatchEnd"].ToString();
                            }
                            if (reader["BatchReport"] != DBNull.Value)
                            {
                                organizeCalculation.BatchReport = reader["BatchReport"].ToString();
                            }
                            if (reader["ParmTime"] != DBNull.Value)
                            {
                                organizeCalculation.ParmTime = Convert.ToDateTime(reader["ParmTime"]);
                            }
                            if (reader["ProcessOrder"] != DBNull.Value)
                            {
                                organizeCalculation.ProcessOrder = reader["ProcessOrder"].ToString();
                            }
                            if (reader["Phase"] != DBNull.Value)
                            {
                                organizeCalculation.Phase = reader["Phase"].ToString();
                            }
                            if (reader["UniqueId"] != DBNull.Value)
                            {
                                organizeCalculation.UniqueId = reader["UniqueId"].ToString();
                            }

                            organizeCalculationResult.Add(organizeCalculation);
                        }

                    }
                    getArchiveDataResult.organizeCalculation = organizeCalculationResult;
                    reader.NextResult();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CreateConsumtion createConsumtion = new CreateConsumtion();
                            if (reader["ParmType"] != DBNull.Value)
                            {
                                createConsumtion.ParmType = reader["ParmType"].ToString();
                            }
                            if (reader["ProductCode"] != DBNull.Value)
                            {
                                createConsumtion.ProductCode = reader["ProductCode"].ToString();
                            }
                            if (reader["Phase"] != DBNull.Value)
                            {
                                createConsumtion.Phase = reader["Phase"].ToString();
                            }
                            if (reader["ParmTime"] != DBNull.Value)
                            {
                                createConsumtion.ParmTime = Convert.ToDateTime(reader["ParmTime"]);
                            }
                            if (reader["NetWeight"] != DBNull.Value)
                            {
                                createConsumtion.NetWeight = reader["NetWeight"].ToString();
                            }
                            if (reader["SourceLocation"] != DBNull.Value)
                            {
                                createConsumtion.SourceLocation = reader["SourceLocation"].ToString();
                            }
                            if (reader["SourceLotId"] != DBNull.Value)
                            {
                                createConsumtion.SourceLotId = reader["SourceLotId"].ToString();
                            }
                            if (reader["BatchUoM"] != DBNull.Value)
                            {
                                createConsumtion.BatchUoM = reader["BatchUoM"].ToString();
                            }
                            if (reader["SAPReport"] != DBNull.Value)
                            {
                                createConsumtion.SAPReport = reader["SAPReport"].ToString();
                            }
                            if (reader["FilterValue"] != DBNull.Value)
                            {
                                createConsumtion.FilterValue = reader["FilterValue"].ToString();
                            }
                            if (reader["StartHeelPhase"] != DBNull.Value)
                            {
                                createConsumtion.StartHeelPhase = reader["StartHeelPhase"].ToString();
                            }
                            if (reader["UniqueId"] != DBNull.Value)
                            {
                                createConsumtion.UniqueId = reader["UniqueId"].ToString();
                            }
                            createConsumtionResult.Add(createConsumtion);
                        }

                    }
                    getArchiveDataResult.createConsumtion = createConsumtionResult;

                    reader.NextResult();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TestConformance testConformance = new TestConformance();
                            if (reader["ParmType"] != DBNull.Value)
                            {
                                testConformance.ParmType = reader["ParmType"].ToString();
                            }
                            if (reader["Phase"] != DBNull.Value)
                            {
                                testConformance.Phase = reader["Phase"].ToString();
                            }
                            if (reader["ParmTime"] != DBNull.Value)
                            {
                                testConformance.ParmTime = Convert.ToDateTime(reader["ParmTime"]);
                            }
                            if (reader["ParmName"] != DBNull.Value)
                            {
                                testConformance.ParmName = reader["ParmName"].ToString();
                            }
                            if (reader["ParmValue"] != DBNull.Value)
                            {
                                testConformance.ParmValue = reader["ParmValue"].ToString();
                            }
                            if (reader["UniqueId"] != DBNull.Value)
                            {
                                testConformance.UniqueId = reader["UniqueId"].ToString();
                            }
                            testConformanceResult.Add(testConformance);
                        }

                    }


                    getArchiveDataResult.testConformance = testConformanceResult;

                    reader.NextResult();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ErrorMessages errorMessages = new ErrorMessages();
                            if (reader["ErrorMessage"] != DBNull.Value)
                            {
                                errorMessages.ErrorMessage = reader["ErrorMessage"].ToString();
                            }
                            if (reader["UniqueId"] != DBNull.Value)
                            {
                                errorMessages.UniqueId = reader["UniqueId"].ToString();
                            }
                            errorMessagesResult.Add(errorMessages);

                        }
                    }

                    getArchiveDataResult.errorMessages = errorMessagesResult;

                    reader.Close();
                }
                var message = command.Parameters["@op_vchErrorMessage"].Value.ToString();

                if (message != "Success")
                {
                    getArchiveDataResult.Message = message;
                }
            }

            return getArchiveDataResult;
        }

    }
}