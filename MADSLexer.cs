using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLEditor
{
    // Cf. https://github.com/jacobslusser/ScintillaNET/wiki/Custom-Syntax-Highlighting

    class MADSLexer
    {
  
        public const int StyleDefault = 0;
        public const int StyleKeyword = 1;
        public const int StyleIdentifier = 2;
        public const int StyleNumber = 3;
        public const int StyleString = 4;
        public const int StyleComment = 5;

        private const int STATE_UNKNOWN = 0;
        private const int STATE_IDENTIFIER = 1;
        private const int STATE_NUMBER = 2;
        private const int STATE_STRING = 3;
        private const int STATE_COMMENT = 4;

        private HashSet<string> keywords;

        public void Style(Scintilla scintilla, int startPos, int endPos)
        {
            // Back up to the line start
            var line = scintilla.LineFromPosition(startPos);
            startPos = scintilla.Lines[line].Position;

            var length = 0;
            var state = STATE_UNKNOWN;

            // Start styling
            scintilla.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char)scintilla.GetCharAt(startPos);

            REPROCESS:
                switch (state)
                {
                    case STATE_UNKNOWN:
                        if (c == '"')
                        {
                            // Start of "string"
                            scintilla.SetStyling(1, StyleString);
                            state = STATE_STRING;
                        }
                        else if (Char.IsDigit(c))
                        {
                            state = STATE_NUMBER;
                            goto REPROCESS;
                        }
                        else if (Char.IsLetter(c))
                        {
                            state = STATE_IDENTIFIER;
                            goto REPROCESS;
                        }
                        else if (c == ';')
                        {
                            state = STATE_COMMENT;
                            goto REPROCESS;
                        }
                        else 
                        {
                            // Everything else
                            scintilla.SetStyling(1, StyleDefault);
                        }
                        break;

                    case STATE_COMMENT:
                        if (c=='\n')
                        {
                            length++;
                            scintilla.SetStyling(length, StyleComment);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                    case STATE_STRING:
                        if (c == '"')
                        {
                            length++;
                            scintilla.SetStyling(length, StyleString);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;

                    case STATE_NUMBER:
                        if (Char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || c == 'x')
                        {
                            length++;
                        }
                        else
                        {
                            scintilla.SetStyling(length, StyleNumber);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;

                    case STATE_IDENTIFIER:
                        if (Char.IsLetterOrDigit(c))
                        {
                            length++;
                        }
                        else
                        {
                            var style = StyleIdentifier;
                            var identifier = scintilla.GetTextRange(startPos - length, length);
                            if (keywords.Contains(identifier))
                                style = StyleKeyword;

                            scintilla.SetStyling(length, style);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;
                }

                startPos++;
            }
        }

        // Cf mads.properties
        String cpu_instruction = @"lda ldx   ldy sta   stx sty   adc and 
                                  asl sbc   jsr jmp   lsr ora   cmp cpy 
                                  cpx dec   inc eor   rol ror   brk clc 
                                  cli clv   cld php   plp pha   pla rti 
                                  rts sec   sei sed   iny inx   dey dex 
                                  txa tya   txs tay   tax tsx   nop bpl 
                                  bmi bne   bcc bcs   beq bvc   bvs bit 
                                  stz sep   rep trb   tsb bra   cop mvn 
                                  mvp pea   phb phd   phk phx   phy plb 
                                  pld plx   ply rtl   stp tcd   tcs tdc 
                                  tsc txy   tyx wai   wdm xba   xce dea 
                                  ina brl   jsl jml   per pei";

#pragma warning disable CS0414 // Le champ 'MADSLexer.ext_instruction' est assigné, mais sa valeur n'est jamais utilisée
        String ext_instruction = @"equ opt   org ins   end dta   icl run 
                                  nmb ini   rmb lmb   ift eli   els eif 
                                  ert smb   blk sin   rnd req   rne rpl 
                                  rmi rcc   rcs rvc   rvs seq   sne spl 
                                  smi scc   scs svc   svs jeq   jne jpl 
                                  jmi jcc   jcs jvc   jvs ext   add sub 
                                  mva mvx   mvy mwa   mwx mwy   inw dew 
                                  adw sbw   phr plr   adb sbb   set"
#pragma warning restore CS0414 // Le champ 'MADSLexer.ext_instruction' est assigné, mais sa valeur n'est jamais utilisée
;


#pragma warning disable CS0414 // Le champ 'MADSLexer.register' est assigné, mais sa valeur n'est jamais utilisée
        String register = @"x y";
#pragma warning restore CS0414 // Le champ 'MADSLexer.register' est assigné, mais sa valeur n'est jamais utilisée


#pragma warning disable CS0414 // Le champ 'MADSLexer.mads_directive' est assigné, mais sa valeur n'est jamais utilisée
        String mads_directive = @".macro  .endm   .proc   .endp   .rept   .endr   .exit 
                                  .local .endl   .struct .ends   .error  .print  .if 
                                  .else  .elseif .endif  .byte   .word   .long   .dword 
                                  .or    .and    .xor    .not    .ds     .dbyte 
                                  .def   .array  .enda   .hi     .lo     .get    .put 
                                  .sav	 .pages	 .endpg  .reloc  .extrn  .public .var 
                                  .reg	 .while	 .endw   .by     .wo     .he	 .en 
                                  .sb	 .test	 .endt   .switch .case   .endsw  .lend 
                                  .pend	 .aend	 .wend   .tend   .send   .fl     .symbol 
                                  .link  .global .globl  .adr    .len    .mend   .pgend 
                                  .rend  .using  .use    .echo   .align  .zpvar  .enum 
                                  .ende  .eend   .elif   .define .undeff"
#pragma warning restore CS0414 // Le champ 'MADSLexer.mads_directive' est assigné, mais sa valeur n'est jamais utilisée
;

        public MADSLexer()
        {
            // Put keywords in a HashSet
            var list = Regex.Split(cpu_instruction ?? string.Empty, @"\s+").Where(l => !string.IsNullOrEmpty(l));
            this.keywords = new HashSet<string>(list);
        }
    }
}
