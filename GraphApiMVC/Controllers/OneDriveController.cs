using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure.Core;

namespace GraphApiMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneDriveController : ControllerBase
    {
        private static string accessToken = "EwBwA8l6BAAUs5+HQn0N+h2FxWzLS31ZgQVuHsYAAb5GiNwsfEwCcV2wEwxNRugEILFWZBVN43tY+0gEoCiHvMjONoNPm1UXrLH+t9leWtHmodv3UCuzukNmz608h7jGcanMpubIpuUJA6wTl2f24FEzUfmR2N3edXDG0nISuGUgjHOCZXL2d+nRz3PVeZydNaCA2kjS6n+FgdxBTrb66ZilOpWCBZdcdr/ndTwp2fbI9uBKaaH83GBSS3vEqt4qkjabgE+L9/UPtFe7Yj2kqnlepXqW1CzjbOWW+M/cckTVSCp9rTMTLAmh+fmF1MAEOOwYduAfIQzNNelEzE0LqZSBCWOtiBv2RxJKKJVIxWo/pMJf2EYRMMIcyTT68zQDZgAACOZPLF3amZihQAIzHLFkaVtPe+Rn+T7riwf+V8BSi/mWsG4bGB0ppDDDC9Bc2yPXPJr1gQKvkiQGbH1samKXJukVU8TbKogw4AT3R29HW1hbJyemjQDPFC7RP7uZoW/N8rNTahxs0ccoM1p53zcKQMkb+Lph79dvuu/LHh/OqatfG0wDRPiWXGdUco9LP/WW1C/UXRIdunb2fMZku3nPEFjxa7CEFVcH0FXY39odSlFBDNCproJNQnLhJ6Zd8BtPeMdjuq/jCI1rYq0ccLpqiYejtc36/WfqgI3jmRXMJwFf50OadLXd9u6IzZCaEh2eDU8+SatWqLqL1vJaRT7cr7NZ4zlTVtaKOIwcSxL7GwxgePZ5M3lGunw3oQdwMNvS4MA2kdRUOIQ+0L52/+2VkW8cJDAz5rDftayCGgTAxia+4wJBgvVvaKwl42tOSTOTxhTcUovlakT9UTPQgtRDhHPEFAGRg+GTmh7l0EhhOBjYukaiMyyUAzM0quKuxNhyNzLqey12XBylzY7YngTpYf3T6LDW7Y5qGCd4BV4ERej796CPo3vnPxy4Gmpuuq9qYq78rgvB1bNABrB9IydmHeYBE84dtqhWSHQTDQXJ5KyNDcNJzaoKTtt4UrveifgPdKqqj3VWFeM+/qqluznxlLSu3SkC9GO/aHPZrHF3eIATFIgVUdVGQItyd5DPOz6koNiE3V+l6c+dJSYbRQp1PwZbmzLSDFuJj2UJJtSbDHsC/bn/JATmOXlSjcibeyCXl+2jTHSKXrMTKFmPAg==";

       [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var graphService = new OneDriveService(accessToken);
                var fileName = "TextFile2.txt";
                var currentFolder = System.IO.Directory.GetCurrentDirectory();
                var filePath = Path.Combine(currentFolder, fileName);
                await graphService.UploadFileToDrive(filePath, fileName);
                return Ok("File uploaded successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error uploading file: {ex.Message}");
            }
        }
        [HttpGet("downloadUserFiles")]
        public async Task<IActionResult> DownloadUserFiles()
        {
            try
            {
                var oneDriveService = new OneDriveService(accessToken);
                var files = await oneDriveService.DownloadUserFilesFromOneDrive();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error downloading files: {ex.Message}");
            }
        }
        [HttpGet("downloadFileById")]
        public async Task<IActionResult> DownloadFileById([FromQuery] string fileId)
        {
            try
            {
                string filePath = "C:\\Users\\nineleaps\\Desktop\\Asp.Net Projects\\GraphApiMVC\\GraphApiMVC\\NewFolder\\";
                var oneDriveService = new OneDriveService(accessToken);
                var files = await oneDriveService.DownloadFileById(fileId, filePath);
                return Ok(files);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error downloading files: {ex.Message}");
            }
        }
        [HttpPost("uploadLargeFile")]
        public async Task<IActionResult> UploadLargeFile()
        {
            try
            {
                var fileName = "Vaaranam-Aayiram-128kbps-MassTamilan.com.zip";
                var currentFolder = System.IO.Directory.GetCurrentDirectory();
                var filePath = Path.Combine(currentFolder, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var oneDriveService = new OneDriveService(accessToken);
                    await oneDriveService.UploadLargeFileToDrive(fileName, fileStream);
                }
                return Ok("Large file uploaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error uploading large file: {ex.Message}");
            }
        }
    }
}