using CarnivalBackstage.Lobby.Binary;
using CarnivalBackstage.Lobby.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace CarnivalBackstage.Lobby.Protocols.Role.S2C;

internal class DragConfigListResultCmd : ISendableCmd
{
    public byte[] Serialize()
    {
        BufferWriter dataWriter = new();

        const byte length = 19;
        dataWriter.PushByte(length);
        AddFile(dataWriter, "ShopAtlas.xml", "d41d8cd98f00b204e9800998ecf8427e");
        AddFile(dataWriter, "params.xml", "d41d8cd98f00b204e9800998ecf8427e");
        AddFile(dataWriter, "AttackEffect.xml", "cc171cba0ae6534e9dcb5a06d8c94626");
        AddFile(dataWriter, "BeAttackEffect.xml", "cf47c99446bfc55a69a450bea9c96844");
        AddFile(dataWriter, "BuffEffect.xml", "9a00cddb0e6d203760c96b31726b8b45");
        AddFile(dataWriter, "CareerAnis.xml", "aa602e7ac7053d8002d1940689edc634");

        AddFile(dataWriter, "Careers.xml", "c59543ad27ef01e4c6d66f8dff8a3388");
        AddFile(dataWriter, "CompoundFee.xml", "61902ad40897dc3248424d5280170a73");

        AddFile(dataWriter, "GemDefine.xml", "44570be61b2b75902b4ce5bbc053c380");
        AddFile(dataWriter, "GemDrop.xml", "461c2556749a5f6a03a06344c1a437fe");
        AddFile(dataWriter, "Level_Scene.xml", "6f3ab7a4f7363ff4faf96db810d3146d");
        AddFile(dataWriter, "LevelIncome.csv", "08cf6682b35b1f6fe3a33a5277381f51");
        AddFile(dataWriter, "MapLayout.xml", "64986a5c8543db567e810d852d198600");
        AddFile(dataWriter, "MAXEXP.csv", "99e70efa9fb28b2cdcdebcd1cb18c283");
        AddFile(dataWriter, "MiscUnit.xml", "222b96c460dab434c035ce36cae9925c");
        AddFile(dataWriter, "pointincome.csv", "b901165756cadeb5a9dabe61ac0a5c1a");
        AddFile(dataWriter, "RPGSkills.xml", "e1a0e02ced77045c8a8aa0d42f511f1b");
        AddFile(dataWriter, "SpecialLevelDrop.xml", "d5f3dfa292e97806bc3306a42e6e09ed");
        AddFile(dataWriter, "Tactics.xml", "4f8f8355df0c2d910bfe0e9f8d898d2a");

        BufferWriter p = new();

        Header h = new((byte)ProtoID.ROLE, (byte)Cmd.drag_config_list_result_s);
        byte[] data = dataWriter.ToByteArray();
        h.SetBodyLength(data.Length);
        h.Serialize(p);

        p.PushByteArray(data);

        return p.ToByteArray();
    }

    static void AddFile(BufferWriter writer, string confName, string hash)
    {
        byte[] name = new byte[33];
        byte[] nameUtf = Encoding.UTF8.GetBytes(confName);
        Array.Copy(nameUtf, name, Math.Min(nameUtf.Length, 33));
        writer.PushByteArray(name);

        byte[] md5 = new byte[33];
        byte[] md5Utf = Encoding.UTF8.GetBytes(hash);
        Array.Copy(md5Utf, md5, Math.Min(md5Utf.Length, 33));
        writer.PushByteArray(md5);
    }


    //public class DragConfigListResultCmd
    //{
    //    public List<FileDigest> lstFileDigest = new List<FileDigest>();
    //
    //    public bool Parse(BufferReader reader)
    //    {
    //        byte val = 0;
    //        if (!reader.PopByte(ref val))
    //        {
    //            return false;
    //        }
    //        for (int i = 0; i < val; i++)
    //        {
    //            FileDigest fileDigest = new FileDigest();
    //            byte[] val2 = new byte[33];
    //            if (!reader.PopByteArray(ref val2, val2.Length))
    //            {
    //                return false;
    //            }
    //            fileDigest._fileName = Encoding.UTF8.GetString(val2);
    //            int num = fileDigest._fileName.IndexOf('\0');
    //            if (num != -1)
    //            {
    //                fileDigest._fileName = fileDigest._fileName.Substring(0, fileDigest._fileName.IndexOf('\0'));
    //            }
    //            byte[] val3 = new byte[33];
    //            if (!reader.PopByteArray(ref val3, val3.Length))
    //            {
    //                return false;
    //            }
    //            fileDigest._md5 = Encoding.UTF8.GetString(val3);
    //            int num2 = fileDigest._md5.IndexOf('\0');
    //            if (num2 != -1)
    //            {
    //                fileDigest._md5 = fileDigest._md5.Substring(0, fileDigest._md5.IndexOf('\0'));
    //            }
    //            lstFileDigest.Add(fileDigest);
    //        }
    //        return true;
    //    }
    //}
}
