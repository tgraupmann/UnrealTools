using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnrealDelegateMacros
{
    class Program
    {
        const int MAX_NUMBER_OF_PARAMETERS = 50;

        static string GetStringNumber(int i)
        {
            return HumanFriendlyInteger.IntegerToWritten(i).Replace(" ", "");
        }

        static void Main(string[] args)
        {
            #region DelegateCombinations

            using (FileStream fs = File.Open("../../HeaderParser.cpp", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int index = 1; index <= MAX_NUMBER_OF_PARAMETERS; ++index)
                    {
                        writer.Write("	DelegateParameterCountStrings.Add(TEXT(\"_{0}Param", GetStringNumber(index));
                        if (index > 1)
                        {
                            writer.Write("s");
                        }
                        writer.WriteLine("\"));");
                    }
                }
            }

            using (FileStream fs = File.Open("../../DelegateCombinations.h", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(@"// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.");
                    writer.WriteLine();
                    writer.WriteLine("// NOTE: This source file was automatically generated using DelegateHeaderTool");
                    writer.WriteLine();
                    writer.WriteLine("// Only designed to be included directly by Delegate.h");
                    writer.WriteLine("#if !defined( __Delegate_h__ ) || !defined( FUNC_INCLUDING_INLINE_IMPL )");
                    writer.WriteLine("    #error \"This inline header must only be included by Delegate.h\"");
                    writer.WriteLine("#endif");
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine();

                    for (int index = 0; index <= MAX_NUMBER_OF_PARAMETERS; ++index)
                    {
                        string[] funcSuffixes =
                        {
                            string.Empty,
                            "RetVal",
                        };
                        foreach (string funcSuffix in funcSuffixes)
                        {
                            StringBuilder sb = new StringBuilder();
                            writer.Write("#define FUNC_SUFFIX ");
                            bool hasReturn = !string.IsNullOrEmpty(funcSuffix);
                            if (hasReturn)
                            {
                                sb.AppendFormat("{0}_", funcSuffix);
                            }
                            if (index == 0)
                            {
                                sb.Append("No");
                            }
                            else
                            {
                                sb.Append(GetStringNumber(index));
                            }
                            sb.Append("Param");
                            if (index != 1)
                            {
                                sb.Append("s");
                            }
                            string partFuncSuffix = sb.ToString();
                            writer.WriteLine(partFuncSuffix);

                            writer.Write("#define FUNC_RETVAL_TYPEDEF");
                            if (hasReturn)
                            {
                                writer.Write("typedef RetValType RetValType;");
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_TEMPLATE_DECL RetValType");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", Param{0}Type", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_TEMPLATE_DECL_TYPENAME typename RetValType");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", typename Param{0}Type", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_TEMPLATE_DECL_NO_SHADOW typename RetValTypeNoShadow");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", typename Param{0}TypeNoShadow", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_TEMPLATE_ARGS RetValType");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", Param{0}Type", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_HAS_PARAMS ");
                            if (index > 0)
                            {
                                writer.WriteLine("1");
                            }
                            else
                            {
                                writer.WriteLine("0");
                            }
                            writer.Write("#define FUNC_PARAM_LIST");
                            for (int j = 1; j <= index; ++j)
                            {
                                if (j > 1)
                                {
                                    writer.Write(",");
                                }
                                writer.Write(" Param{0}Type InParam{0}", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_PARAM_MEMBERS");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(" Param{0}Type Param{0};", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_PARAM_PASSTHRU");
                            for (int j = 1; j <= index; ++j)
                            {
                                if (j > 1)
                                {
                                    writer.Write(",");
                                }
                                writer.Write(" InParam{0}", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_PARAM_PARMS_PASSIN");
                            for (int j = 1; j <= index; ++j)
                            {
                                if (j > 1)
                                {
                                    writer.Write(",");
                                }
                                writer.Write(" Parms.Param{0}", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_PARAM_INITIALIZER_LIST");
                            for (int j = 1; j <= index; ++j)
                            {
                                if (j > 1)
                                {
                                    writer.Write(",");
                                }
                                writer.Write(" Param{0}( InParam{0} )", j);
                            }
                            writer.WriteLine();
                            writer.Write("#define FUNC_IS_VOID ");
                            if (hasReturn)
                            {
                                writer.WriteLine("0");
                            }
                            else
                            {
                                writer.WriteLine("1");
                            }
                            writer.WriteLine("# include \"DelegateInstanceInterfaceImpl.inl\"");
                            writer.WriteLine();

                            for (int i = 0; i <= 4; ++i)
                            {
                                string strNumber = GetStringNumber(i);

                                if (i > 0)
                                {
                                    WriteUndefine(writer);
                                }

                                writer.Write("#define FUNC_HAS_PAYLOAD ");
                                if (i == 0)
                                {
                                    writer.WriteLine("0");
                                }
                                else
                                {
                                    writer.WriteLine("1");
                                }
                                writer.Write("#define FUNC_PAYLOAD_SUFFIX ");
                                if (hasReturn)
                                {
                                    writer.Write("RetVal_");
                                }
                                if (index == 0)
                                {
                                    writer.Write("NoParams");
                                }
                                else
                                {
                                    writer.Write("{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                if (i > 0)
                                {
                                    writer.Write("_");
                                    writer.Write(strNumber);
                                    writer.Write("Var");
                                    if (i > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.WriteLine();
                                WritePayloadTemplate(writer, index, i);
                                writer.Write("#define FUNC_PAYLOAD_MEMBERS");
                                for (int j = 1; j <= i; ++j)
                                {
                                    writer.Write(" Var{0}Type Var{0};", j);
                                }
                                writer.WriteLine();
                                writer.Write("#define FUNC_PAYLOAD_LIST");
                                for (int j = 1; j <= i; ++j)
                                {
                                    if (j > 1)
                                    {
                                        writer.Write(",");
                                    }
                                    writer.Write(" Var{0}Type InVar{0}", j);
                                }
                                writer.WriteLine();
                                writer.Write("#define FUNC_PAYLOAD_PASSTHRU");
                                for (int j = 1; j <= i; ++j)
                                {
                                    if (j > 1)
                                    {
                                        writer.Write(",");
                                    }
                                    writer.Write(" InVar{0}", j);
                                }
                                writer.WriteLine();
                                writer.Write("#define FUNC_PAYLOAD_PASSIN");
                                for (int j = 1; j <= i; ++j)
                                {
                                    if (j > 1)
                                    {
                                        writer.Write(",");
                                    }
                                    writer.Write(" Var{0}", j);
                                }
                                writer.WriteLine();
                                writer.Write("#define FUNC_PAYLOAD_INITIALIZER_LIST");
                                for (int j = 1; j <= i; ++j)
                                {
                                    if (j > 1)
                                    {
                                        writer.Write(",");
                                    }
                                    writer.Write(" Var{0}( InVar{0} )", j);
                                }
                                writer.WriteLine();

                                WriteUndefine2(writer);
                            }

                            WriteUndefine(writer);

                            writer.WriteLine("#include \"DelegateSignatureImpl.inl\"");
                            writer.WriteLine();

                            writer.Write("#define DECLARE_DELEGATE");
                            if (hasReturn)
                            {
                                writer.Write("_RetVal");
                            }
                            if (index > 0)
                            {
                                writer.Write("_{0}Param", GetStringNumber(index));
                                if (index > 1)
                                {
                                    writer.Write("s");
                                }
                            }
                            writer.Write("(");
                            if (hasReturn)
                            {
                                writer.Write(" RetValType,");
                            }
                            writer.Write(" DelegateName");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", Param{0}Type", j);
                            }
                            writer.Write(" ) FUNC_DECLARE_DELEGATE( ");
                            if (hasReturn)
                            {
                                writer.Write("RetVal_");
                            }
                            if (index == 0)
                            {
                                writer.Write("NoParams");
                            }
                            else
                            {
                                writer.Write("{0}Param", GetStringNumber(index));
                                if (index > 1)
                                {
                                    writer.Write("s");
                                }
                            }
                            writer.Write(", DelegateName, ");
                            if (hasReturn)
                            {
                                writer.Write("RetValType");
                            }
                            else
                            {
                                writer.Write("void");
                            }
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", Param{0}Type", j);
                            }
                            writer.WriteLine(" )");
                            if (!hasReturn)
                            {
                                writer.Write("#define DECLARE_MULTICAST_DELEGATE");
                                if (index > 0)
                                {
                                    writer.Write("_{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.Write("( DelegateName");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", Param{0}Type", j);
                                }
                                writer.Write(" ) FUNC_DECLARE_MULTICAST_DELEGATE( ");
                                if (index == 0)
                                {
                                    writer.Write("NoParams");
                                }
                                else
                                {
                                    writer.Write("{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.Write(", DelegateName, void ");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", Param{0}Type", j);
                                }
                                writer.WriteLine(" )");
                                writer.Write("#define DECLARE_EVENT");
                                if (index > 0)
                                {
                                    writer.Write("_{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.Write("( OwningType, EventName");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", Param{0}Type", j);
                                }
                                writer.Write(" ) FUNC_DECLARE_EVENT( OwningType, EventName, ");
                                if (index == 0)
                                {
                                    writer.Write("NoParams");
                                }
                                else
                                {
                                    writer.Write("{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.Write(", void");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", Param{0}Type", j);
                                }
                                writer.WriteLine(" )");
                            }
                            writer.Write("#define DECLARE_DYNAMIC_DELEGATE");
                            if (hasReturn)
                            {
                                writer.Write("_RetVal");
                            }
                            if (index > 0)
                            {
                                writer.Write("_{0}Param", GetStringNumber(index));
                                if (index > 1)
                                {
                                    writer.Write("s");
                                }
                            }
                            writer.Write("(");
                            if (hasReturn)
                            {
                                writer.Write(" RetValType,");
                            }
                            writer.Write(" DelegateName");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", Param{0}Type, Param{0}Name", j);
                            }
                            writer.Write(" ) BODY_MACRO_COMBINE(CURRENT_FILE_ID,_,__LINE__,_DELEGATE) FUNC_DECLARE_DYNAMIC_DELEGATE");
                            if (hasReturn)
                            {
                                writer.Write("_RETVAL");
                            }
                            writer.Write("( FWeakObjectPtr, ");
                            if (hasReturn)
                            {
                                writer.Write("RetVal_");
                            }
                            if (index == 0)
                            {
                                writer.Write("NoParams");
                            }
                            else
                            {
                                writer.Write("{0}Param", GetStringNumber(index));
                                if (index > 1)
                                {
                                    writer.Write("s");
                                }
                            }
                            writer.Write(", DelegateName, DelegateName##_DelegateWrapper, ");
                            if (hasReturn)
                            {
                                writer.Write("RetValType, ");
                            }
                            if (index > 0)
                            {
                                writer.Write("FUNC_CONCAT(");
                                for (int j = 1; j <= index; ++j)
                                {
                                    if (j > 1)
                                    {
                                        writer.Write(",");
                                    }
                                    writer.Write(" Param{0}Type InParam{0}", j);
                                }
                                writer.Write(" )");
                            }
                            writer.Write(", FUNC_CONCAT( *this");
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", InParam{0}", j);
                            }
                            writer.Write(" ), ");
                            if (hasReturn)
                            {
                                writer.Write("RetValType");
                            }
                            else
                            {
                                writer.Write("void");
                            }
                            for (int j = 1; j <= index; ++j)
                            {
                                writer.Write(", Param{0}Type", j);
                            }
                            writer.WriteLine(" )");
                            if (!hasReturn)
                            {
                                writer.Write("#define DECLARE_DYNAMIC_MULTICAST_DELEGATE");
                                if (index > 0)
                                {
                                    writer.Write("_{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.Write("( DelegateName");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", Param{0}Type, Param{0}Name", j);
                                }
                                writer.Write(" ) BODY_MACRO_COMBINE(CURRENT_FILE_ID,_,__LINE__,_DELEGATE) FUNC_DECLARE_DYNAMIC_MULTICAST_DELEGATE( FWeakObjectPtr, ");
                                if (index == 0)
                                {
                                    writer.Write("NoParams");
                                }
                                else
                                {
                                    writer.Write("{0}Param", GetStringNumber(index));
                                    if (index > 1)
                                    {
                                        writer.Write("s");
                                    }
                                }
                                writer.Write(", DelegateName, DelegateName##_DelegateWrapper, ");
                                if (index > 0)
                                {
                                    writer.Write("FUNC_CONCAT(");
                                    for (int j = 1; j <= index; ++j)
                                    {
                                        if (j > 1)
                                        {
                                            writer.Write(",");
                                        }
                                        writer.Write(" Param{0}Type InParam{0}", j);
                                    }
                                    writer.Write(" )");
                                }
                                writer.Write(", FUNC_CONCAT( *this");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", InParam{0}", j);
                                }
                                writer.Write(" ), void");
                                for (int j = 1; j <= index; ++j)
                                {
                                    writer.Write(", Param{0}Type", j);
                                }
                                writer.WriteLine(" )");
                            }
                            WriteUndefine3(writer);
                        }
                    }

                    writer.Flush();
                }
            }

            #endregion

            #region DelegateCombinations_Variadics
            //TextWriter writer = (TextWriter)Console.Out;
            using (FileStream fs = File.Open("../../DelegateCombinations_Variadics.h", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(@"// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.");
                    writer.WriteLine();
                    writer.WriteLine("#pragma once");
                    writer.WriteLine();

                    for (int i = 0; i <= MAX_NUMBER_OF_PARAMETERS; ++i)
                    {
                        string strNumber = GetStringNumber(i);
                        WriteDelegate(writer, i, strNumber);
                        WriteMulticastDelegate(writer, i, strNumber);
                        WriteEvent(writer, i, strNumber);
                        WriteDynamicDelegate(writer, i, strNumber);
                        WriteDynamicMulticastDelegate(writer, i, strNumber);
                        WriteDelegateReturnVal(writer, i, strNumber);
                        WriteDynamicDelegateReturnVal(writer, i, strNumber);
                    }

                    writer.Flush();
                }
            }
            #endregion
        }

        static void WritePayloadTemplate(TextWriter writer, int index, int i)
        {
            writer.Write("#define FUNC_PAYLOAD_TEMPLATE_DECL RetValType");
            for (int j = 1; j <= index; ++j)
            {
                writer.Write(", Param{0}Type", j);
            }
            for (int j = 1; j <= i; ++j)
            {
                writer.Write(", Var{0}Type", j);
            }
            writer.WriteLine();
            writer.Write("#define FUNC_PAYLOAD_TEMPLATE_DECL_TYPENAME typename RetValType");
            for (int j = 1; j <= index; ++j)
            {
                writer.Write(", typename Param{0}Type", j);
            }
            for (int j = 1; j <= i; ++j)
            {
                writer.Write(", typename Var{0}Type", j);
            }
            writer.WriteLine();
            writer.Write("#define FUNC_PAYLOAD_TEMPLATE_DECL_NO_SHADOW typename RetValTypeNoShadow");
            for (int j = 1; j <= index; ++j)
            {
                writer.Write(", typename Param{0}TypeNoShadow", j);
            }
            for (int j = 1; j <= i; ++j)
            {
                writer.Write(", typename Var{0}TypeNoShadow", j);
            }
            writer.WriteLine();
            writer.Write("#define FUNC_PAYLOAD_TEMPLATE_ARGS RetValType");
            for (int j = 1; j <= index; ++j)
            {
                writer.Write(", Param{0}Type", j);
            }
            for (int j = 1; j <= i; ++j)
            {
                writer.Write(", Var{0}Type", j);
            }
            writer.WriteLine();
        }

        static void WriteUndefine(TextWriter writer)
        {
            writer.WriteLine("#undef FUNC_HAS_PAYLOAD");
            writer.WriteLine("#undef FUNC_PAYLOAD_SUFFIX");
            writer.WriteLine("#undef FUNC_PAYLOAD_TEMPLATE_DECL");
            writer.WriteLine("#undef FUNC_PAYLOAD_TEMPLATE_DECL_TYPENAME");
            writer.WriteLine("#undef FUNC_PAYLOAD_TEMPLATE_DECL_NO_SHADOW");
            writer.WriteLine("#undef FUNC_PAYLOAD_TEMPLATE_ARGS");
            writer.WriteLine("#undef FUNC_PAYLOAD_MEMBERS");
            writer.WriteLine("#undef FUNC_PAYLOAD_LIST");
            writer.WriteLine("#undef FUNC_PAYLOAD_PASSTHRU");
            writer.WriteLine("#undef FUNC_PAYLOAD_PASSIN");
            writer.WriteLine("#undef FUNC_PAYLOAD_INITIALIZER_LIST");
            writer.WriteLine();
        }

        static void WriteUndefine2(TextWriter writer)
        {
            writer.WriteLine("#define FUNC_IS_CONST 0");
            writer.WriteLine("#define FUNC_CONST_SUFFIX");
            writer.WriteLine("# include \"DelegateInstancesImpl.inl\"");
            writer.WriteLine("#undef FUNC_IS_CONST");
            writer.WriteLine("#undef FUNC_CONST_SUFFIX");
            writer.WriteLine("#define FUNC_IS_CONST 1");
            writer.WriteLine("#define FUNC_CONST_SUFFIX _Const");
            writer.WriteLine("# include \"DelegateInstancesImpl.inl\"");
            writer.WriteLine("#undef FUNC_IS_CONST");
            writer.WriteLine("#undef FUNC_CONST_SUFFIX");
            writer.WriteLine();
        }

        static void WriteUndefine3(TextWriter writer)
        {
            writer.WriteLine("#undef FUNC_SUFFIX");
            writer.WriteLine("#undef FUNC_TEMPLATE_DECL");
            writer.WriteLine("#undef FUNC_TEMPLATE_DECL_TYPENAME");
            writer.WriteLine("#undef FUNC_TEMPLATE_DECL_NO_SHADOW");
            writer.WriteLine("#undef FUNC_TEMPLATE_ARGS");
            writer.WriteLine("#undef FUNC_HAS_PARAMS");
            writer.WriteLine("#undef FUNC_PARAM_LIST");
            writer.WriteLine("#undef FUNC_PARAM_MEMBERS");
            writer.WriteLine("#undef FUNC_PARAM_PASSTHRU");
            writer.WriteLine("#undef FUNC_PARAM_PARMS_PASSIN");
            writer.WriteLine("#undef FUNC_PARAM_INITIALIZER_LIST");
            writer.WriteLine("#undef FUNC_IS_VOID");
            writer.WriteLine();
        }

        static void WriteDelegate(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_DELEGATE");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( DelegateName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            sb.Append(" FUNC_DECLARE_DELEGATE");
            sb.Append("( ");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", DelegateName");
            sb.Append(", void");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }

        static void WriteMulticastDelegate(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_MULTICAST_DELEGATE");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( DelegateName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            sb.Append(" FUNC_DECLARE_MULTICAST_DELEGATE");
            sb.Append("( ");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", DelegateName");
            sb.Append(", void");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }

        static void WriteEvent(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_EVENT");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( OwningType, EventName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            sb.Append(" FUNC_DECLARE_EVENT");
            sb.Append("( OwningType, EventName, ");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", void");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }

        static void WriteDynamicDelegate(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_DYNAMIC_DELEGATE");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( DelegateName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Name");
                }
            }
            sb.Append(" )");
            sb.Append(" BODY_MACRO_COMBINE(CURRENT_FILE_ID,_,__LINE__,_DELEGATE) FUNC_DECLARE_DYNAMIC_DELEGATE");
            sb.Append("( FWeakObjectPtr, ");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", DelegateName");
            sb.Append(", DelegateName##_DelegateWrapper, ");
            if (i == 0)
            {
                sb.Append(", FUNC_CONCAT( *this ),");
            }
            else
            {
                sb.Append("FUNC_CONCAT( ");
                for (int j = 1; j <= i; ++j)
                {
                    if (j > 1)
                    {
                        sb.Append(", ");
                    }
                    sb.Append("Param");
                    sb.Append(j);
                    sb.Append("Type InParam");
                    sb.Append(j);
                }
                sb.Append(" ), ");
                sb.Append("FUNC_CONCAT( *this");
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", InParam");
                    sb.Append(j);
                }
                sb.Append(" ),");
            }
            sb.Append(" void");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }

        static void WriteDynamicMulticastDelegate(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_DYNAMIC_MULTICAST_DELEGATE");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( DelegateName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Name");
                }
            }
            sb.Append(" )");
            sb.Append(" BODY_MACRO_COMBINE(CURRENT_FILE_ID,_,__LINE__,_DELEGATE) FUNC_DECLARE_DYNAMIC_MULTICAST_DELEGATE");
            sb.Append("( FWeakObjectPtr, ");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", DelegateName");
            sb.Append(", DelegateName##_DelegateWrapper, ");
            if (i == 0)
            {
                sb.Append(", FUNC_CONCAT( *this ),");
            }
            else
            {
                sb.Append("FUNC_CONCAT( ");
                for (int j = 1; j <= i; ++j)
                {
                    if (j > 1)
                    {
                        sb.Append(", ");
                    }
                    sb.Append("Param");
                    sb.Append(j);
                    sb.Append("Type InParam");
                    sb.Append(j);
                }
                sb.Append(" ), ");
                sb.Append("FUNC_CONCAT( *this");
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", InParam");
                    sb.Append(j);
                }
                sb.Append(" ),");
            }
            sb.Append(" void");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }

        static void WriteDelegateReturnVal(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_DELEGATE_RetVal");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( ReturnValueType, DelegateName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            sb.Append(" FUNC_DECLARE_DELEGATE( RetVal_");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", DelegateName, ReturnValueType");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }

        static void WriteDynamicDelegateReturnVal(TextWriter writer, int i, string strNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#define");
            sb.Append(" DECLARE_DYNAMIC_DELEGATE_RetVal");
            if (i > 0)
            {
                sb.Append("_");
                sb.Append(strNumber);
                sb.Append("Param");
            }
            if (i > 1)
            {
                sb.Append("s");
            }
            sb.Append("( ReturnValueType, DelegateName");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Name");
                }
            }
            sb.Append(" )");
            sb.Append(" BODY_MACRO_COMBINE(CURRENT_FILE_ID,_,__LINE__,_DELEGATE) FUNC_DECLARE_DYNAMIC_DELEGATE_RETVAL( FWeakObjectPtr, RetVal_");
            if (i == 0)
            {
                sb.Append("NoParams");
            }
            else
            {
                sb.Append(strNumber);
                sb.Append("Param");
                if (i > 1)
                {
                    sb.Append("s");
                }
            }
            sb.Append(", DelegateName, DelegateName##_DelegateWrapper, ReturnValueType, ");
            if (i == 0)
            {
                sb.Append(", FUNC_CONCAT( *this )");
            }
            else
            {
                sb.Append("FUNC_CONCAT(");
                for (int j = 1; j <= i; ++j)
                {
                    if (j > 1)
                    {
                        sb.Append(",");
                    }
                    sb.Append(" Param");
                    sb.Append(j);
                    sb.Append("Type");
                    sb.Append(" InParam");
                    sb.Append(j);
                }
                sb.Append(" ), ");
                sb.Append("FUNC_CONCAT( *this");
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", InParam");
                    sb.Append(j);
                }
                sb.Append(" )");
            }
            sb.Append(", ReturnValueType");
            if (i > 0)
            {
                for (int j = 1; j <= i; ++j)
                {
                    sb.Append(", Param");
                    sb.Append(j);
                    sb.Append("Type");
                }
            }
            sb.Append(" )");
            writer.WriteLine(sb.ToString());
        }
    }

    public static class HumanFriendlyInteger
    {
        static string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] thousandsGroups = { "", " Thousand", " Million", " Billion" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }
            string friendlyInt = leftDigits;
            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negative " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }

    }
}
