using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Data_Compression
{
    public static class FrequencyOfCharacters
    {
        public static Dictionary<int, int> FREQUENCY;
        public static Dictionary<int, Tuple<double, double>> RANGES;
        public static int COUNT;

        public static void Init()
        {
            FREQUENCY = new Dictionary<int, int>();
            COUNT = 0;
            FREQUENCY[0] = 1; COUNT += FREQUENCY[0]; //  
            FREQUENCY[1] = 1; COUNT += FREQUENCY[1]; // 
            FREQUENCY[2] = 1; COUNT += FREQUENCY[2]; // 
            FREQUENCY[3] = 1; COUNT += FREQUENCY[3]; // 
            FREQUENCY[4] = 1; COUNT += FREQUENCY[4]; // 
            FREQUENCY[5] = 1; COUNT += FREQUENCY[5]; // 
            FREQUENCY[6] = 1; COUNT += FREQUENCY[6]; // 
            FREQUENCY[7] = 1; COUNT += FREQUENCY[7]; // 
            FREQUENCY[8] = 1; COUNT += FREQUENCY[8]; // 
            FREQUENCY[9] = 1; COUNT += FREQUENCY[9]; // 	
            FREQUENCY[10] = 1; COUNT += FREQUENCY[10]; // break line
            FREQUENCY[11] = 1; COUNT += FREQUENCY[11]; // 
            FREQUENCY[12] = 1; COUNT += FREQUENCY[12]; // 
            FREQUENCY[13] = 1; COUNT += FREQUENCY[13]; // break line
            FREQUENCY[14] = 1; COUNT += FREQUENCY[14]; // 
            FREQUENCY[15] = 1; COUNT += FREQUENCY[15]; // 
            FREQUENCY[16] = 1; COUNT += FREQUENCY[16]; // 
            FREQUENCY[17] = 1; COUNT += FREQUENCY[17]; // 
            FREQUENCY[18] = 1; COUNT += FREQUENCY[18]; // 
            FREQUENCY[19] = 1; COUNT += FREQUENCY[19]; // 
            FREQUENCY[20] = 1; COUNT += FREQUENCY[20]; // 
            FREQUENCY[21] = 1; COUNT += FREQUENCY[21]; // 
            FREQUENCY[22] = 1; COUNT += FREQUENCY[22]; // 
            FREQUENCY[23] = 1; COUNT += FREQUENCY[23]; // 
            FREQUENCY[24] = 1; COUNT += FREQUENCY[24]; // 
            FREQUENCY[25] = 1; COUNT += FREQUENCY[25]; // 
            FREQUENCY[26] = 1; COUNT += FREQUENCY[26]; // 
            FREQUENCY[27] = 1; COUNT += FREQUENCY[27]; // 
            FREQUENCY[28] = 1; COUNT += FREQUENCY[28]; // 
            FREQUENCY[29] = 1; COUNT += FREQUENCY[29]; // 
            FREQUENCY[30] = 1; COUNT += FREQUENCY[30]; // 
            FREQUENCY[31] = 1; COUNT += FREQUENCY[31]; // 
            FREQUENCY[32] = 1; COUNT += FREQUENCY[32]; //  
            FREQUENCY[33] = 1; COUNT += FREQUENCY[33]; // !
            FREQUENCY[34] = 1; COUNT += FREQUENCY[34]; // "
            FREQUENCY[35] = 1; COUNT += FREQUENCY[35]; // #
            FREQUENCY[36] = 1; COUNT += FREQUENCY[36]; // $
            FREQUENCY[37] = 1; COUNT += FREQUENCY[37]; // %
            FREQUENCY[38] = 1; COUNT += FREQUENCY[38]; // &
            FREQUENCY[39] = 1; COUNT += FREQUENCY[39]; // '
            FREQUENCY[40] = 1; COUNT += FREQUENCY[40]; // (
            FREQUENCY[41] = 1; COUNT += FREQUENCY[41]; // )
            FREQUENCY[42] = 1; COUNT += FREQUENCY[42]; // *
            FREQUENCY[43] = 1; COUNT += FREQUENCY[43]; // +
            FREQUENCY[44] = 1; COUNT += FREQUENCY[44]; // ,
            FREQUENCY[45] = 1; COUNT += FREQUENCY[45]; // -
            FREQUENCY[46] = 1; COUNT += FREQUENCY[46]; // .
            FREQUENCY[47] = 1; COUNT += FREQUENCY[47]; // /
            FREQUENCY[48] = 1; COUNT += FREQUENCY[48]; // 0
            FREQUENCY[49] = 1; COUNT += FREQUENCY[49]; // 1
            FREQUENCY[50] = 1; COUNT += FREQUENCY[50]; // 2
            FREQUENCY[51] = 1; COUNT += FREQUENCY[51]; // 3
            FREQUENCY[52] = 1; COUNT += FREQUENCY[52]; // 4
            FREQUENCY[53] = 1; COUNT += FREQUENCY[53]; // 5
            FREQUENCY[54] = 1; COUNT += FREQUENCY[54]; // 6
            FREQUENCY[55] = 1; COUNT += FREQUENCY[55]; // 7
            FREQUENCY[56] = 1; COUNT += FREQUENCY[56]; // 8
            FREQUENCY[57] = 1; COUNT += FREQUENCY[57]; // 9
            FREQUENCY[58] = 1; COUNT += FREQUENCY[58]; // :
            FREQUENCY[59] = 1; COUNT += FREQUENCY[59]; // ;
            FREQUENCY[60] = 1; COUNT += FREQUENCY[60]; // <
            FREQUENCY[61] = 1; COUNT += FREQUENCY[61]; // =
            FREQUENCY[62] = 1; COUNT += FREQUENCY[62]; // >
            FREQUENCY[63] = 1; COUNT += FREQUENCY[63]; // ?
            FREQUENCY[64] = 1; COUNT += FREQUENCY[64]; // @
            FREQUENCY[65] = 1; COUNT += FREQUENCY[65]; // A
            FREQUENCY[66] = 1; COUNT += FREQUENCY[66]; // B
            FREQUENCY[67] = 1; COUNT += FREQUENCY[67]; // C
            FREQUENCY[68] = 1; COUNT += FREQUENCY[68]; // D
            FREQUENCY[69] = 1; COUNT += FREQUENCY[69]; // E
            FREQUENCY[70] = 1; COUNT += FREQUENCY[70]; // F
            FREQUENCY[71] = 1; COUNT += FREQUENCY[71]; // G
            FREQUENCY[72] = 1; COUNT += FREQUENCY[72]; // H
            FREQUENCY[73] = 1; COUNT += FREQUENCY[73]; // I
            FREQUENCY[74] = 1; COUNT += FREQUENCY[74]; // J
            FREQUENCY[75] = 1; COUNT += FREQUENCY[75]; // K
            FREQUENCY[76] = 1; COUNT += FREQUENCY[76]; // L
            FREQUENCY[77] = 1; COUNT += FREQUENCY[77]; // M
            FREQUENCY[78] = 1; COUNT += FREQUENCY[78]; // N
            FREQUENCY[79] = 1; COUNT += FREQUENCY[79]; // O
            FREQUENCY[80] = 1; COUNT += FREQUENCY[80]; // P
            FREQUENCY[81] = 1; COUNT += FREQUENCY[81]; // Q
            FREQUENCY[82] = 1; COUNT += FREQUENCY[82]; // R
            FREQUENCY[83] = 1; COUNT += FREQUENCY[83]; // S
            FREQUENCY[84] = 1; COUNT += FREQUENCY[84]; // T
            FREQUENCY[85] = 1; COUNT += FREQUENCY[85]; // U
            FREQUENCY[86] = 1; COUNT += FREQUENCY[86]; // V
            FREQUENCY[87] = 1; COUNT += FREQUENCY[87]; // W
            FREQUENCY[88] = 1; COUNT += FREQUENCY[88]; // X
            FREQUENCY[89] = 1; COUNT += FREQUENCY[89]; // Y
            FREQUENCY[90] = 1; COUNT += FREQUENCY[90]; // Z
            FREQUENCY[91] = 1; COUNT += FREQUENCY[91]; // [
            FREQUENCY[92] = 1; COUNT += FREQUENCY[92]; // \
            FREQUENCY[93] = 1; COUNT += FREQUENCY[93]; // ]
            FREQUENCY[94] = 1; COUNT += FREQUENCY[94]; // ^
            FREQUENCY[95] = 1; COUNT += FREQUENCY[95]; // _
            FREQUENCY[96] = 1; COUNT += FREQUENCY[96]; // `
            FREQUENCY[97] = 1; COUNT += FREQUENCY[97]; // a
            FREQUENCY[98] = 1; COUNT += FREQUENCY[98]; // b
            FREQUENCY[99] = 1; COUNT += FREQUENCY[99]; // c
            FREQUENCY[100] = 1; COUNT += FREQUENCY[100]; // d
            FREQUENCY[101] = 1; COUNT += FREQUENCY[101]; // e
            FREQUENCY[102] = 1; COUNT += FREQUENCY[102]; // f
            FREQUENCY[103] = 1; COUNT += FREQUENCY[103]; // g
            FREQUENCY[104] = 1; COUNT += FREQUENCY[104]; // h
            FREQUENCY[105] = 1; COUNT += FREQUENCY[105]; // i
            FREQUENCY[106] = 1; COUNT += FREQUENCY[106]; // j
            FREQUENCY[107] = 1; COUNT += FREQUENCY[107]; // k
            FREQUENCY[108] = 1; COUNT += FREQUENCY[108]; // l
            FREQUENCY[109] = 1; COUNT += FREQUENCY[109]; // m
            FREQUENCY[110] = 1; COUNT += FREQUENCY[110]; // n
            FREQUENCY[111] = 1; COUNT += FREQUENCY[111]; // o
            FREQUENCY[112] = 1; COUNT += FREQUENCY[112]; // p
            FREQUENCY[113] = 1; COUNT += FREQUENCY[113]; // q
            FREQUENCY[114] = 1; COUNT += FREQUENCY[114]; // r
            FREQUENCY[115] = 1; COUNT += FREQUENCY[115]; // s
            FREQUENCY[116] = 1; COUNT += FREQUENCY[116]; // t
            FREQUENCY[117] = 1; COUNT += FREQUENCY[117]; // u
            FREQUENCY[118] = 1; COUNT += FREQUENCY[118]; // v
            FREQUENCY[119] = 1; COUNT += FREQUENCY[119]; // w
            FREQUENCY[120] = 1; COUNT += FREQUENCY[120]; // x
            FREQUENCY[121] = 1; COUNT += FREQUENCY[121]; // y
            FREQUENCY[122] = 1; COUNT += FREQUENCY[122]; // z
            FREQUENCY[123] = 1; COUNT += FREQUENCY[123]; // {
            FREQUENCY[124] = 1; COUNT += FREQUENCY[124]; // |
            FREQUENCY[125] = 1; COUNT += FREQUENCY[125]; // }
            FREQUENCY[126] = 1; COUNT += FREQUENCY[126]; // ~
            FREQUENCY[127] = 1; COUNT += FREQUENCY[127]; // 
            FREQUENCY[128] = 1; COUNT += FREQUENCY[128]; // 
            FREQUENCY[129] = 1; COUNT += FREQUENCY[129]; // 
            FREQUENCY[130] = 1; COUNT += FREQUENCY[130]; // 
            FREQUENCY[131] = 1; COUNT += FREQUENCY[131]; // 
            FREQUENCY[132] = 1; COUNT += FREQUENCY[132]; // 
            FREQUENCY[133] = 1; COUNT += FREQUENCY[133]; // 
            FREQUENCY[134] = 1; COUNT += FREQUENCY[134]; // 
            FREQUENCY[135] = 1; COUNT += FREQUENCY[135]; // 
            FREQUENCY[136] = 1; COUNT += FREQUENCY[136]; // 
            FREQUENCY[137] = 1; COUNT += FREQUENCY[137]; // 
            FREQUENCY[138] = 1; COUNT += FREQUENCY[138]; // 
            FREQUENCY[139] = 1; COUNT += FREQUENCY[139]; // 
            FREQUENCY[140] = 1; COUNT += FREQUENCY[140]; // 
            FREQUENCY[141] = 1; COUNT += FREQUENCY[141]; // 
            FREQUENCY[142] = 1; COUNT += FREQUENCY[142]; // 
            FREQUENCY[143] = 1; COUNT += FREQUENCY[143]; // 
            FREQUENCY[144] = 1; COUNT += FREQUENCY[144]; // 
            FREQUENCY[145] = 1; COUNT += FREQUENCY[145]; // 
            FREQUENCY[146] = 1; COUNT += FREQUENCY[146]; // 
            FREQUENCY[147] = 1; COUNT += FREQUENCY[147]; // 
            FREQUENCY[148] = 1; COUNT += FREQUENCY[148]; // 
            FREQUENCY[149] = 1; COUNT += FREQUENCY[149]; // 
            FREQUENCY[150] = 1; COUNT += FREQUENCY[150]; // 
            FREQUENCY[151] = 1; COUNT += FREQUENCY[151]; // 
            FREQUENCY[152] = 1; COUNT += FREQUENCY[152]; // 
            FREQUENCY[153] = 1; COUNT += FREQUENCY[153]; // 
            FREQUENCY[154] = 1; COUNT += FREQUENCY[154]; // 
            FREQUENCY[155] = 1; COUNT += FREQUENCY[155]; // 
            FREQUENCY[156] = 1; COUNT += FREQUENCY[156]; // 
            FREQUENCY[157] = 1; COUNT += FREQUENCY[157]; // 
            FREQUENCY[158] = 1; COUNT += FREQUENCY[158]; // 
            FREQUENCY[159] = 1; COUNT += FREQUENCY[159]; // 
            FREQUENCY[160] = 1; COUNT += FREQUENCY[160]; //  
            FREQUENCY[161] = 1; COUNT += FREQUENCY[161]; // ¡
            FREQUENCY[162] = 1; COUNT += FREQUENCY[162]; // ¢
            FREQUENCY[163] = 1; COUNT += FREQUENCY[163]; // £
            FREQUENCY[164] = 1; COUNT += FREQUENCY[164]; // ¤
            FREQUENCY[165] = 1; COUNT += FREQUENCY[165]; // ¥
            FREQUENCY[166] = 1; COUNT += FREQUENCY[166]; // ¦
            FREQUENCY[167] = 1; COUNT += FREQUENCY[167]; // §
            FREQUENCY[168] = 1; COUNT += FREQUENCY[168]; // ¨
            FREQUENCY[169] = 1; COUNT += FREQUENCY[169]; // ©
            FREQUENCY[170] = 1; COUNT += FREQUENCY[170]; // ª
            FREQUENCY[171] = 1; COUNT += FREQUENCY[171]; // «
            FREQUENCY[172] = 1; COUNT += FREQUENCY[172]; // ¬
            FREQUENCY[173] = 1; COUNT += FREQUENCY[173]; // ­
            FREQUENCY[174] = 1; COUNT += FREQUENCY[174]; // ®
            FREQUENCY[175] = 1; COUNT += FREQUENCY[175]; // ¯
            FREQUENCY[176] = 1; COUNT += FREQUENCY[176]; // °
            FREQUENCY[177] = 1; COUNT += FREQUENCY[177]; // ±
            FREQUENCY[178] = 1; COUNT += FREQUENCY[178]; // ²
            FREQUENCY[179] = 1; COUNT += FREQUENCY[179]; // ³
            FREQUENCY[180] = 1; COUNT += FREQUENCY[180]; // ´
            FREQUENCY[181] = 1; COUNT += FREQUENCY[181]; // µ
            FREQUENCY[182] = 1; COUNT += FREQUENCY[182]; // ¶
            FREQUENCY[183] = 1; COUNT += FREQUENCY[183]; // ·
            FREQUENCY[184] = 1; COUNT += FREQUENCY[184]; // ¸
            FREQUENCY[185] = 1; COUNT += FREQUENCY[185]; // ¹
            FREQUENCY[186] = 1; COUNT += FREQUENCY[186]; // º
            FREQUENCY[187] = 1; COUNT += FREQUENCY[187]; // »
            FREQUENCY[188] = 1; COUNT += FREQUENCY[188]; // ¼
            FREQUENCY[189] = 1; COUNT += FREQUENCY[189]; // ½
            FREQUENCY[190] = 1; COUNT += FREQUENCY[190]; // ¾
            FREQUENCY[191] = 1; COUNT += FREQUENCY[191]; // ¿
            FREQUENCY[192] = 1; COUNT += FREQUENCY[192]; // À
            FREQUENCY[193] = 1; COUNT += FREQUENCY[193]; // Á
            FREQUENCY[194] = 1; COUNT += FREQUENCY[194]; // Â
            FREQUENCY[195] = 1; COUNT += FREQUENCY[195]; // Ã
            FREQUENCY[196] = 1; COUNT += FREQUENCY[196]; // Ä
            FREQUENCY[197] = 1; COUNT += FREQUENCY[197]; // Å
            FREQUENCY[198] = 1; COUNT += FREQUENCY[198]; // Æ
            FREQUENCY[199] = 1; COUNT += FREQUENCY[199]; // Ç
            FREQUENCY[200] = 1; COUNT += FREQUENCY[200]; // È
            FREQUENCY[201] = 1; COUNT += FREQUENCY[201]; // É
            FREQUENCY[202] = 1; COUNT += FREQUENCY[202]; // Ê
            FREQUENCY[203] = 1; COUNT += FREQUENCY[203]; // Ë
            FREQUENCY[204] = 1; COUNT += FREQUENCY[204]; // Ì
            FREQUENCY[205] = 1; COUNT += FREQUENCY[205]; // Í
            FREQUENCY[206] = 1; COUNT += FREQUENCY[206]; // Î
            FREQUENCY[207] = 1; COUNT += FREQUENCY[207]; // Ï
            FREQUENCY[208] = 1; COUNT += FREQUENCY[208]; // Ð
            FREQUENCY[209] = 1; COUNT += FREQUENCY[209]; // Ñ
            FREQUENCY[210] = 1; COUNT += FREQUENCY[210]; // Ò
            FREQUENCY[211] = 1; COUNT += FREQUENCY[211]; // Ó
            FREQUENCY[212] = 1; COUNT += FREQUENCY[212]; // Ô
            FREQUENCY[213] = 1; COUNT += FREQUENCY[213]; // Õ
            FREQUENCY[214] = 1; COUNT += FREQUENCY[214]; // Ö
            FREQUENCY[215] = 1; COUNT += FREQUENCY[215]; // ×
            FREQUENCY[216] = 1; COUNT += FREQUENCY[216]; // Ø
            FREQUENCY[217] = 1; COUNT += FREQUENCY[217]; // Ù
            FREQUENCY[218] = 1; COUNT += FREQUENCY[218]; // Ú
            FREQUENCY[219] = 1; COUNT += FREQUENCY[219]; // Û
            FREQUENCY[220] = 1; COUNT += FREQUENCY[220]; // Ü
            FREQUENCY[221] = 1; COUNT += FREQUENCY[221]; // Ý
            FREQUENCY[222] = 1; COUNT += FREQUENCY[222]; // Þ
            FREQUENCY[223] = 1; COUNT += FREQUENCY[223]; // ß
            FREQUENCY[224] = 1; COUNT += FREQUENCY[224]; // à
            FREQUENCY[225] = 1; COUNT += FREQUENCY[225]; // á
            FREQUENCY[226] = 1; COUNT += FREQUENCY[226]; // â
            FREQUENCY[227] = 1; COUNT += FREQUENCY[227]; // ã
            FREQUENCY[228] = 1; COUNT += FREQUENCY[228]; // ä
            FREQUENCY[229] = 1; COUNT += FREQUENCY[229]; // å
            FREQUENCY[230] = 1; COUNT += FREQUENCY[230]; // æ
            FREQUENCY[231] = 1; COUNT += FREQUENCY[231]; // ç
            FREQUENCY[232] = 1; COUNT += FREQUENCY[232]; // è
            FREQUENCY[233] = 1; COUNT += FREQUENCY[233]; // é
            FREQUENCY[234] = 1; COUNT += FREQUENCY[234]; // ê
            FREQUENCY[235] = 1; COUNT += FREQUENCY[235]; // ë
            FREQUENCY[236] = 1; COUNT += FREQUENCY[236]; // ì
            FREQUENCY[237] = 1; COUNT += FREQUENCY[237]; // í
            FREQUENCY[238] = 1; COUNT += FREQUENCY[238]; // î
            FREQUENCY[239] = 1; COUNT += FREQUENCY[239]; // ï
            FREQUENCY[240] = 1; COUNT += FREQUENCY[240]; // ð
            FREQUENCY[241] = 1; COUNT += FREQUENCY[241]; // ñ
            FREQUENCY[242] = 1; COUNT += FREQUENCY[242]; // ò
            FREQUENCY[243] = 1; COUNT += FREQUENCY[243]; // ó
            FREQUENCY[244] = 1; COUNT += FREQUENCY[244]; // ô
            FREQUENCY[245] = 1; COUNT += FREQUENCY[245]; // õ
            FREQUENCY[246] = 1; COUNT += FREQUENCY[246]; // ö
            FREQUENCY[247] = 1; COUNT += FREQUENCY[247]; // ÷
            FREQUENCY[248] = 1; COUNT += FREQUENCY[248]; // ø
            FREQUENCY[249] = 1; COUNT += FREQUENCY[249]; // ù
            FREQUENCY[250] = 1; COUNT += FREQUENCY[250]; // ú
            FREQUENCY[251] = 1; COUNT += FREQUENCY[251]; // û
            FREQUENCY[252] = 1; COUNT += FREQUENCY[252]; // ü
            FREQUENCY[253] = 1; COUNT += FREQUENCY[253]; // ý
            FREQUENCY[254] = 1; COUNT += FREQUENCY[254]; // þ
            FREQUENCY[255] = 1; COUNT += FREQUENCY[255]; // ÿ

            RANGES = new Dictionary<int, Tuple<double, double>>();
            double low = 0;
            foreach (var symbol in FREQUENCY)
            {
                double probability = symbol.Value / COUNT;
                double high = low + probability;
                RANGES[symbol.Key] = new Tuple<double, double>(low, high);
                low = high;
            }
        }
    }
}
