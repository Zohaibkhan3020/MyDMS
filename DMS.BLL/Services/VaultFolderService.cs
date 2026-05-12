using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    public class VaultFolderService
    {
        public string CreateVaultFolders(
            string rootPath,
            string vaultName)
        {
            var vaultPath =
                Path.Combine(rootPath, "Vaults", vaultName);

            Directory.CreateDirectory(vaultPath);

            Directory.CreateDirectory(Path.Combine(vaultPath, "Files"));

            Directory.CreateDirectory(Path.Combine(vaultPath, "OCR"));

            Directory.CreateDirectory(Path.Combine(vaultPath, "Preview"));

            Directory.CreateDirectory(Path.Combine(vaultPath, "Versions"));

            return vaultPath;
        }
    }
}
