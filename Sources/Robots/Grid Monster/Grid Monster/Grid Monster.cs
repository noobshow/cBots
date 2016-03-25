﻿// ------------------------------------------------------------                   
// Paste this code into your cAlgo editor. 
// -----------------------------------------------------------
using System;
using System.Collections.Generic;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using cAlgo.API.Requests;
// ---------------------------------------------------------------------------                   
// Converted from MQ4 to cAlgo with http://2calgo.com
// ---------------------------------------------------------------------------

namespace cAlgo.Robots
{
    [Robot(AccessRights = AccessRights.FullAccess)]
    public class ConvertedRobot : Robot
    {
        Mq4Double Mq4Init()
        {
            Mq4Double l_str2time_8 = 0;
            Mq4String ls_0 = "";
            ls_0 = "2010.10.12";
            l_str2time_8 = StrToTime(ls_0);
            if (TimeCurrent() >= l_str2time_8)
            {
                Alert("The trial version has been expired!");
                return 0;
            }
            gs_grid_280 = StringConcatenate("Grid-", Symbol.Code, "-", uniqueGridMagic);
            return 0;
            return 0;
        }
        Mq4Double IsPositionFunc(Mq4Double ad_0, Mq4Double ad_8, Mq4Double ai_16)
        {
            Mq4Double l_pos_28 = 0;
            Mq4Double l_ord_total_24 = 0;
            Mq4Double l_cmd_20 = 0;

            l_ord_total_24 = OrdersTotal();
            for (l_pos_28 = 0; l_pos_28 < l_ord_total_24; l_pos_28++)
            {
                OrderSelect(l_pos_28, SELECT_BY_POS);
                if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
                {
                    l_cmd_20 = OrderType();
                    if (MathAbs(OrderOpenPrice() - ad_0) < 0.9 * ad_8)
                        if ((ai_16 && l_cmd_20 == OP_BUY || l_cmd_20 == OP_BUYLIMIT || l_cmd_20 == OP_BUYSTOP) || (!ai_16 && l_cmd_20 == OP_SELL || l_cmd_20 == OP_SELLLIMIT || l_cmd_20 == OP_SELLSTOP))
                            return 1;
                }
            }
            return 0;
            return 0;
        }
        void DeleteAfterFunc(Mq4Double ad_0)
        {
            Mq4Double l_pos_16 = 0;
            Mq4Double l_ord_total_12 = 0;
            Mq4Double li_unused_8 = 0;

            l_ord_total_12 = OrdersTotal();
            for (l_pos_16 = l_ord_total_12 - 1; l_pos_16 >= 0; l_pos_16--)
                OrderSelect(l_pos_16, SELECT_BY_POS, MODE_TRADES);
            if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
            {
                if (MathAbs(iTime(Symbol.Code, PERIOD_M5, 0) - OrderOpenTime()) >= 60.0 * (60.0 * ad_0) && iTime(Symbol.Code, PERIOD_M5, 0) > 0)
                {
                    li_unused_8 = 0;
                    if (OrderType() == OP_BUY)
                        li_unused_8 = OrderClose(OrderTicket(), OrderLots(), MarketInfo(OrderSymbol(), MODE_BID), 5, Red);
                    if (OrderType() == OP_SELL)
                        li_unused_8 = OrderClose(OrderTicket(), OrderLots(), MarketInfo(OrderSymbol(), MODE_ASK), 5, Red);
                    if (OrderType() > OP_SELL)
                        li_unused_8 = OrderDelete(OrderTicket());
                }
            }
            return;
        }
        void CloseAllPendingOrdersFunc()
        {
            Mq4Double l_pos_8 = 0;
            Mq4Double l_ord_total_4 = 0;
            Mq4Double li_unused_0 = 0;

            l_ord_total_4 = OrdersTotal();
            for (l_pos_8 = l_ord_total_4 - 1; l_pos_8 >= 0; l_pos_8--)
            {
                OrderSelect(l_pos_8, SELECT_BY_POS);
                if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
                    if (OrderType() > OP_SELL)
                        li_unused_0 = OrderDelete(OrderTicket());
            }
            return;
        }
        void ClosePendingOrdersAndPositionsFunc()
        {
            Mq4Double l_pos_8 = 0;
            Mq4Double l_ord_total_4 = 0;
            Mq4Double li_unused_0 = 0;

            l_ord_total_4 = OrdersTotal();
            for (l_pos_8 = l_ord_total_4 - 1; l_pos_8 >= 0; l_pos_8--)
            {
                OrderSelect(l_pos_8, SELECT_BY_POS);
                li_unused_0 = 0;
                if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
                {
                    if (OrderType() == OP_BUY)
                        li_unused_0 = OrderClose(OrderTicket(), OrderLots(), MarketInfo(OrderSymbol(), MODE_BID), 5, Red);
                    if (OrderType() == OP_SELL)
                        li_unused_0 = OrderClose(OrderTicket(), OrderLots(), MarketInfo(OrderSymbol(), MODE_ASK), 5, Red);
                    if (OrderType() > OP_SELL)
                        li_unused_0 = OrderDelete(OrderTicket());
                }
            }
            return;
        }
        void CloseOrdersfromEMAFunc(Mq4Double ad_0)
        {
            Mq4Double l_pos_20 = 0;
            Mq4Double l_ord_total_16 = 0;
            Mq4Double li_unused_12 = 0;
            Mq4Double l_cmd_8 = 0;


            l_ord_total_16 = OrdersTotal();
            for (l_pos_20 = l_ord_total_16 - 1; l_pos_20 >= 0; l_pos_20--)
            {
                OrderSelect(l_pos_20, SELECT_BY_POS);
                if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
                {
                    l_cmd_8 = OrderType();
                    li_unused_12 = 0;
                    if (l_cmd_8 == OP_BUYLIMIT && OrderOpenPrice() <= ad_0)
                        li_unused_12 = OrderDelete(OrderTicket());
                    if (l_cmd_8 == OP_BUYSTOP && OrderOpenPrice() <= ad_0)
                        li_unused_12 = OrderDelete(OrderTicket());
                    if (l_cmd_8 == OP_SELLLIMIT && OrderOpenPrice() >= ad_0)
                        li_unused_12 = OrderDelete(OrderTicket());
                    if (l_cmd_8 == OP_SELLSTOP && OrderOpenPrice() >= ad_0)
                        li_unused_12 = OrderDelete(OrderTicket());
                }
            }
            return;
        }
        Mq4Double openPositionsFunc()
        {
            Mq4Double l_pos_12 = 0;
            Mq4Double l_ord_total_8 = 0;
            Mq4Double l_count_4 = 0;
            Mq4Double l_cmd_0 = 0;

            l_count_4 = 0;
            l_ord_total_8 = OrdersTotal();
            for (l_pos_12 = l_ord_total_8 - 1; l_pos_12 >= 0; l_pos_12--)
            {
                OrderSelect(l_pos_12, SELECT_BY_POS);
                if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
                {
                    l_cmd_0 = OrderType();
                    if (l_cmd_0 == OP_BUY)
                        l_count_4++;
                    if (l_cmd_0 == OP_SELL)
                        l_count_4++;
                }
            }
            return l_count_4;
            return 0;
        }
        void TestForProfitFunc(Mq4Double a_magic_0, Mq4Double ad_4, Mq4Double ai_12, Mq4Double ai_16)
        {
            Mq4Double l_pos_28 = 0;
            Mq4Double l_cmd_24 = 0;
            Mq4Double li_20 = 0;


            if (ad_4 > 0.0)
            {
                gd_304 = 0;
                if (ai_16 == 1)
                {
                    li_20 = OrdersHistoryTotal();
                    for (l_pos_28 = 0; l_pos_28 < li_20; l_pos_28++)
                    {
                        OrderSelect(l_pos_28, SELECT_BY_POS, MODE_HISTORY);
                        if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == a_magic_0)
                            gd_304 += OrderProfit();
                    }
                }
                else
                    gd_320 = 0;
                gd_312 = 0;
                if (ai_12 == 1)
                {
                    li_20 = OrdersTotal();
                    g_count_332 = 0;
                    g_count_336 = 0;
                    for (l_pos_28 = 0; l_pos_28 < li_20; l_pos_28++)
                    {
                        OrderSelect(l_pos_28, SELECT_BY_POS);
                        if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == a_magic_0)
                        {
                            gd_312 += OrderProfit();
                            l_cmd_24 = OrderType();
                            if (l_cmd_24 == OP_BUY)
                                g_count_332++;
                            if (l_cmd_24 == OP_SELL)
                                g_count_336++;
                        }
                    }
                }
                if (gd_304 + gd_312 >= ad_4 + gd_320)
                {
                    Mq4Print("Closing grid due to profit target");
                    ClosePendingOrdersAndPositionsFunc();
                    gd_320 = gd_320 + gd_304 + gd_312;
                    gi_328 = TRUE;
                    if (gi_344 == TRUE)
                        gi_340 = FALSE;
                }
            }
            return;
        }
        void TrailItFunc(Mq4Double ai_0)
        {
            Mq4Double l_pos_4 = 0;
            if (ai_0 >= 5)
            {
                for (l_pos_4 = 0; l_pos_4 < OrdersTotal(); l_pos_4++)
                {
                    OrderSelect(l_pos_4, SELECT_BY_POS, MODE_TRADES);
                    if (OrderSymbol() == Symbol.Code && OrderMagicNumber() == uniqueGridMagic || OrderComment() == gs_grid_280)
                    {
                        if (OrderType() == OP_BUY)
                        {
                            if (Bid - OrderOpenPrice() > ai_0 * MarketInfo(OrderSymbol(), MODE_POINT))
                                if (OrderStopLoss() < Bid - ai_0 * MarketInfo(OrderSymbol(), MODE_POINT))
                                    OrderModify(OrderTicket(), OrderOpenPrice(), Bid - ai_0 * MarketInfo(OrderSymbol(), MODE_POINT), OrderTakeProfit(), 255);
                        }
                        else
                        {
                            if (OrderType() == OP_SELL)
                            {
                                if (OrderOpenPrice() - Ask > ai_0 * MarketInfo(OrderSymbol(), MODE_POINT))
                                    if (OrderStopLoss() > Ask + ai_0 * MarketInfo(OrderSymbol(), MODE_POINT) || OrderStopLoss() == 0.0)
                                        OrderModify(OrderTicket(), OrderOpenPrice(), Ask + ai_0 * MarketInfo(OrderSymbol(), MODE_POINT), OrderTakeProfit(), 255);
                            }
                        }
                    }
                }
            }
            return;
        }
        Mq4Double Mq4Start()
        {
            Mq4Double l_count_104 = 0;
            Mq4Double l_bool_100 = false;
            Mq4Double l_bool_96 = false;
            Mq4Double l_price_88 = 0;
            Mq4Double li_84 = 0;
            Mq4Double ld_76 = 0;
            Mq4Double ld_68 = 0;
            Mq4Double ld_60 = 0;
            Mq4Double li_56 = 0;
            Mq4Double l_ima_48 = 0;
            Mq4Double ld_40 = 0;
            Mq4Double l_price_32 = 0;
            Mq4Double ld_24 = 0;
            Mq4Double l_point_16 = 0;
            Mq4Double li_12 = false;
            Mq4Double l_cmd_8 = 0;
            Mq4Double li_unused_4 = 0;
            Mq4Double li_0 = 0;















            if (TakeProfit <= 0.0)
                TakeProfit = GridSize;
            l_bool_96 = wantLongs;
            l_bool_100 = wantShorts;
            if (suspendGrid == TRUE)
                CloseAllPendingOrdersFunc();
            if (shutdownGrid == TRUE)
            {
                ClosePendingOrdersAndPositionsFunc();
                return 0;
            }
            if (gi_328 == TRUE)
            {
                ClosePendingOrdersAndPositionsFunc();
                if (gi_344 == TRUE)
                    gi_340 = FALSE;
            }
            if (gi_340 == FALSE)
                return 0;
            if (MathAbs(TimeCurrent() - g_datetime_288) > 60.0 * UpdateInterval)
            {
                if (profitTarget > 0.0)
                    TestForProfitFunc(uniqueGridMagic, profitTarget, 1, 0);
                g_datetime_288 = TimeCurrent();
                l_point_16 = MarketInfo(Symbol.Code, MODE_POINT);
                ld_24 = (Ask + l_point_16 * GridSize / 2.0) / l_point_16 / GridSize;
                li_0 = ld_24;
                li_0 = li_0 * GridSize;
                ld_24 = li_0 * l_point_16 - GridSize * GridSteps / 2.0 * l_point_16;
                l_ima_48 = iMA(NULL, 0, EMAperiod, 0, MODE_EMA, PRICE_CLOSE, 0);
                li_56 = 0;
                if (GridMaxOpen > 0.0)
                {
                    li_56 = openPositionsFunc();
                    if (li_56 >= GridMaxOpen)
                        CloseAllPendingOrdersFunc();
                }
                if (limitEMA)
                    if (doHouseKeeping)
                        CloseOrdersfromEMAFunc(l_ima_48);
                if (keepOpenTimeLimit > 0.0)
                    DeleteAfterFunc(keepOpenTimeLimit);
                if (trailStop > 0)
                    TrailItFunc(trailStop);
                if (UseMACD || UseOsMA)
                {
                    if (UseMACD)
                    {
                        ld_60 = iMACD(NULL, timeFrame, emaFast, emaSlow, signalPeriod, PRICE_CLOSE, MODE_MAIN, 0);
                        ld_68 = iMACD(NULL, timeFrame, emaFast, emaSlow, signalPeriod, PRICE_CLOSE, MODE_MAIN, 1);
                        ld_76 = iMACD(NULL, timeFrame, emaFast, emaSlow, signalPeriod, PRICE_CLOSE, MODE_MAIN, 2);
                    }
                    if (UseOsMA)
                    {
                        ld_60 = iOsMA(NULL, timeFrame, emaFast, emaSlow, signalPeriod, PRICE_CLOSE, 0);
                        ld_68 = iOsMA(NULL, timeFrame, emaFast, emaSlow, signalPeriod, PRICE_CLOSE, 1);
                        ld_76 = iOsMA(NULL, timeFrame, emaFast, emaSlow, signalPeriod, PRICE_CLOSE, 2);
                    }
                    if (ld_60 > 0.0 && ld_68 > 0.0 && ld_76 < 0.0)
                    {
                        CloseAllPendingOrdersFunc();
                        if (CloseOpenPositions == TRUE)
                            ClosePendingOrdersAndPositionsFunc();
                    }
                    if (ld_60 < 0.0 && ld_68 < 0.0 && ld_76 > 0.0)
                    {
                        CloseAllPendingOrdersFunc();
                        if (CloseOpenPositions == TRUE)
                            ClosePendingOrdersAndPositionsFunc();
                    }
                    l_bool_96 = FALSE;
                    l_bool_100 = FALSE;
                    if (ld_60 > 0.0 && ld_68 > 0.0 && ld_76 > 0.0 && wantLongs == TRUE)
                        l_bool_96 = TRUE;
                    if (ld_60 < 0.0 && ld_68 < 0.0 && ld_76 < 0.0 && wantShorts == TRUE)
                        l_bool_100 = TRUE;
                }
                li_84 = GridSteps;
                if (GridMaxOpen > 0.0 && li_56 >= GridMaxOpen)
                    li_84 = 0;
                if (suspendGrid == TRUE)
                    return 0;
                Mq4Print("Trigs ", ld_60, "  ", ld_68, "  ", ld_76, "  ", l_bool_96, "  ", l_bool_100,
                "  ", li_84, " ema ", l_ima_48, " price ", Bid);
                for (l_count_104 = 0; l_count_104 < li_84; l_count_104++)
                {
                    ld_24 = (Ask + l_point_16 * GridSize / 2.0) / l_point_16 / GridSize;
                    li_0 = ld_24;
                    li_0 = li_0 * GridSize;
                    ld_24 = li_0 * l_point_16 - GridSize * GridSteps / 2.0 * l_point_16;
                    l_price_32 = ld_24 + l_count_104 * l_point_16 * GridSize + gridOffset * l_point_16;
                    if (l_bool_96 && !limitEMA || l_price_32 > l_ima_48)
                    {
                        if (longGridCenter > l_point_16)
                        {
                            ld_40 = GridSteps / 2.0;
                            li_0 = ld_40;
                            ld_24 = longGridCenter - li_0 * GridSize * l_point_16;
                        }
                        else
                        {
                            ld_24 = (Ask + l_point_16 * GridSize / 2.0) / l_point_16 / GridSize;
                            li_0 = ld_24;
                            li_0 = li_0 * GridSize;
                            ld_24 = li_0 * l_point_16 - GridSize * GridSteps / 2.0 * l_point_16;
                        }
                        l_price_32 = ld_24 + l_count_104 * l_point_16 * GridSize + gridOffset * l_point_16;
                        li_12 = TRUE;
                        if (l_price_32 < longGridLow)
                            li_12 = FALSE;
                        if (l_price_32 > longGridHigh && longGridHigh > l_point_16)
                            li_12 = FALSE;
                        if (IsPositionFunc(l_price_32, l_point_16 * GridSize, 1) == 0 && li_12 == TRUE)
                        {
                            l_price_88 = 0;
                            if (StopLoss > 0.0)
                                l_price_88 = l_price_32 - l_point_16 * StopLoss;
                            if (l_price_32 > Ask)
                                l_cmd_8 = 4;
                            else
                                l_cmd_8 = 2;
                            if ((l_price_32 > Ask + minFromPrice * Point && wantBreakout) || (l_price_32 <= Ask - minFromPrice * Point && wantCounter))
                                li_unused_4 = OrderSend(Symbol.Code, l_cmd_8, Lots, l_price_32, 0, l_price_88, l_price_32 + l_point_16 * TakeProfit, gs_grid_280, uniqueGridMagic, 0,
                                Green);
                        }
                    }
                    if (l_bool_100 && !limitEMA || l_price_32 < l_ima_48)
                    {
                        if (shortGridCenter > l_point_16)
                        {
                            ld_40 = GridSteps / 2.0;
                            li_0 = ld_40;
                            ld_24 = shortGridCenter - li_0 * GridSize * l_point_16;
                        }
                        else
                        {
                            ld_24 = (Ask + l_point_16 * GridSize / 2.0) / l_point_16 / GridSize;
                            li_0 = ld_24;
                            li_0 = li_0 * GridSize;
                            ld_24 = li_0 * l_point_16 - GridSize * GridSteps / 2.0 * l_point_16;
                        }
                        l_price_32 = ld_24 + l_count_104 * l_point_16 * GridSize + gridOffset * l_point_16;
                        li_12 = TRUE;
                        if (l_price_32 < shortGridLow)
                            li_12 = FALSE;
                        if (l_price_32 > shortGridHigh && shortGridHigh > l_point_16)
                            li_12 = FALSE;
                        if (IsPositionFunc(l_price_32, l_point_16 * GridSize, 0) == 0 && li_12 == TRUE)
                        {
                            l_price_88 = 0;
                            if (StopLoss > 0.0)
                                l_price_88 = l_price_32 + l_point_16 * StopLoss;
                            if (l_price_32 > Bid)
                                l_cmd_8 = 3;
                            else
                                l_cmd_8 = 5;
                            if ((l_price_32 < Bid - minFromPrice * Point && wantBreakout) || (l_price_32 >= Bid + minFromPrice * Point && wantCounter))
                                li_unused_4 = OrderSend(Symbol.Code, l_cmd_8, Lots, l_price_32, 0, l_price_88, l_price_32 - l_point_16 * TakeProfit, gs_grid_280, uniqueGridMagic, 0,
                                Red);
                        }
                    }
                }
            }
            return 0;
            return 0;
        }

        [Parameter("uniqueGridMagic", DefaultValue = 550000)]
        public int uniqueGridMagic_parameter { get; set; }
        bool _uniqueGridMagicGot;
        Mq4Double uniqueGridMagic_backfield;
        Mq4Double uniqueGridMagic
        {
            get
            {
                if (!_uniqueGridMagicGot)
                    uniqueGridMagic_backfield = uniqueGridMagic_parameter;
                return uniqueGridMagic_backfield;
            }
            set { uniqueGridMagic_backfield = value; }
        }

        [Parameter("Lots", DefaultValue = 0.01)]
        public double Lots_parameter { get; set; }
        bool _LotsGot;
        Mq4Double Lots_backfield;
        Mq4Double Lots
        {
            get
            {
                if (!_LotsGot)
                    Lots_backfield = Lots_parameter;
                return Lots_backfield;
            }
            set { Lots_backfield = value; }
        }

        [Parameter("GridSize", DefaultValue = 6.0)]
        public double GridSize_parameter { get; set; }
        bool _GridSizeGot;
        Mq4Double GridSize_backfield;
        Mq4Double GridSize
        {
            get
            {
                if (!_GridSizeGot)
                    GridSize_backfield = GridSize_parameter;
                return GridSize_backfield;
            }
            set { GridSize_backfield = value; }
        }

        [Parameter("GridSteps", DefaultValue = 10.0)]
        public double GridSteps_parameter { get; set; }
        bool _GridStepsGot;
        Mq4Double GridSteps_backfield;
        Mq4Double GridSteps
        {
            get
            {
                if (!_GridStepsGot)
                    GridSteps_backfield = GridSteps_parameter;
                return GridSteps_backfield;
            }
            set { GridSteps_backfield = value; }
        }

        [Parameter("TakeProfit", DefaultValue = 10.0)]
        public double TakeProfit_parameter { get; set; }
        bool _TakeProfitGot;
        Mq4Double TakeProfit_backfield;
        Mq4Double TakeProfit
        {
            get
            {
                if (!_TakeProfitGot)
                    TakeProfit_backfield = TakeProfit_parameter;
                return TakeProfit_backfield;
            }
            set { TakeProfit_backfield = value; }
        }

        [Parameter("StopLoss", DefaultValue = 0.0)]
        public double StopLoss_parameter { get; set; }
        bool _StopLossGot;
        Mq4Double StopLoss_backfield;
        Mq4Double StopLoss
        {
            get
            {
                if (!_StopLossGot)
                    StopLoss_backfield = StopLoss_parameter;
                return StopLoss_backfield;
            }
            set { StopLoss_backfield = value; }
        }

        [Parameter("trailStop", DefaultValue = 0)]
        public int trailStop_parameter { get; set; }
        bool _trailStopGot;
        Mq4Double trailStop_backfield;
        Mq4Double trailStop
        {
            get
            {
                if (!_trailStopGot)
                    trailStop_backfield = trailStop_parameter;
                return trailStop_backfield;
            }
            set { trailStop_backfield = value; }
        }

        [Parameter("UpdateInterval", DefaultValue = 1.0)]
        public double UpdateInterval_parameter { get; set; }
        bool _UpdateIntervalGot;
        Mq4Double UpdateInterval_backfield;
        Mq4Double UpdateInterval
        {
            get
            {
                if (!_UpdateIntervalGot)
                    UpdateInterval_backfield = UpdateInterval_parameter;
                return UpdateInterval_backfield;
            }
            set { UpdateInterval_backfield = value; }
        }

        [Parameter("wantLongs", DefaultValue = TRUE)]
        public bool wantLongs_parameter { get; set; }
        bool _wantLongsGot;
        Mq4Double wantLongs_backfield;
        Mq4Double wantLongs
        {
            get
            {
                if (!_wantLongsGot)
                    wantLongs_backfield = wantLongs_parameter;
                return wantLongs_backfield;
            }
            set { wantLongs_backfield = value; }
        }

        [Parameter("wantShorts", DefaultValue = TRUE)]
        public bool wantShorts_parameter { get; set; }
        bool _wantShortsGot;
        Mq4Double wantShorts_backfield;
        Mq4Double wantShorts
        {
            get
            {
                if (!_wantShortsGot)
                    wantShorts_backfield = wantShorts_parameter;
                return wantShorts_backfield;
            }
            set { wantShorts_backfield = value; }
        }

        [Parameter("wantBreakout", DefaultValue = TRUE)]
        public bool wantBreakout_parameter { get; set; }
        bool _wantBreakoutGot;
        Mq4Double wantBreakout_backfield;
        Mq4Double wantBreakout
        {
            get
            {
                if (!_wantBreakoutGot)
                    wantBreakout_backfield = wantBreakout_parameter;
                return wantBreakout_backfield;
            }
            set { wantBreakout_backfield = value; }
        }

        [Parameter("wantCounter", DefaultValue = TRUE)]
        public bool wantCounter_parameter { get; set; }
        bool _wantCounterGot;
        Mq4Double wantCounter_backfield;
        Mq4Double wantCounter
        {
            get
            {
                if (!_wantCounterGot)
                    wantCounter_backfield = wantCounter_parameter;
                return wantCounter_backfield;
            }
            set { wantCounter_backfield = value; }
        }

        [Parameter("limitEMA", DefaultValue = TRUE)]
        public bool limitEMA_parameter { get; set; }
        bool _limitEMAGot;
        Mq4Double limitEMA_backfield;
        Mq4Double limitEMA
        {
            get
            {
                if (!_limitEMAGot)
                    limitEMA_backfield = limitEMA_parameter;
                return limitEMA_backfield;
            }
            set { limitEMA_backfield = value; }
        }

        [Parameter("EMAperiod", DefaultValue = 30)]
        public int EMAperiod_parameter { get; set; }
        bool _EMAperiodGot;
        Mq4Double EMAperiod_backfield;
        Mq4Double EMAperiod
        {
            get
            {
                if (!_EMAperiodGot)
                    EMAperiod_backfield = EMAperiod_parameter;
                return EMAperiod_backfield;
            }
            set { EMAperiod_backfield = value; }
        }

        [Parameter("GridMaxOpen", DefaultValue = 50.0)]
        public double GridMaxOpen_parameter { get; set; }
        bool _GridMaxOpenGot;
        Mq4Double GridMaxOpen_backfield;
        Mq4Double GridMaxOpen
        {
            get
            {
                if (!_GridMaxOpenGot)
                    GridMaxOpen_backfield = GridMaxOpen_parameter;
                return GridMaxOpen_backfield;
            }
            set { GridMaxOpen_backfield = value; }
        }

        [Parameter("UseMACD", DefaultValue = TRUE)]
        public bool UseMACD_parameter { get; set; }
        bool _UseMACDGot;
        Mq4Double UseMACD_backfield;
        Mq4Double UseMACD
        {
            get
            {
                if (!_UseMACDGot)
                    UseMACD_backfield = UseMACD_parameter;
                return UseMACD_backfield;
            }
            set { UseMACD_backfield = value; }
        }

        [Parameter("UseOsMA", DefaultValue = FALSE)]
        public bool UseOsMA_parameter { get; set; }
        bool _UseOsMAGot;
        Mq4Double UseOsMA_backfield;
        Mq4Double UseOsMA
        {
            get
            {
                if (!_UseOsMAGot)
                    UseOsMA_backfield = UseOsMA_parameter;
                return UseOsMA_backfield;
            }
            set { UseOsMA_backfield = value; }
        }

        [Parameter("CloseOpenPositions", DefaultValue = FALSE)]
        public bool CloseOpenPositions_parameter { get; set; }
        bool _CloseOpenPositionsGot;
        Mq4Double CloseOpenPositions_backfield;
        Mq4Double CloseOpenPositions
        {
            get
            {
                if (!_CloseOpenPositionsGot)
                    CloseOpenPositions_backfield = CloseOpenPositions_parameter;
                return CloseOpenPositions_backfield;
            }
            set { CloseOpenPositions_backfield = value; }
        }

        [Parameter("doHouseKeeping", DefaultValue = TRUE)]
        public bool doHouseKeeping_parameter { get; set; }
        bool _doHouseKeepingGot;
        Mq4Double doHouseKeeping_backfield;
        Mq4Double doHouseKeeping
        {
            get
            {
                if (!_doHouseKeepingGot)
                    doHouseKeeping_backfield = doHouseKeeping_parameter;
                return doHouseKeeping_backfield;
            }
            set { doHouseKeeping_backfield = value; }
        }

        [Parameter("keepOpenTimeLimit", DefaultValue = 0.0)]
        public double keepOpenTimeLimit_parameter { get; set; }
        bool _keepOpenTimeLimitGot;
        Mq4Double keepOpenTimeLimit_backfield;
        Mq4Double keepOpenTimeLimit
        {
            get
            {
                if (!_keepOpenTimeLimitGot)
                    keepOpenTimeLimit_backfield = keepOpenTimeLimit_parameter;
                return keepOpenTimeLimit_backfield;
            }
            set { keepOpenTimeLimit_backfield = value; }
        }

        [Parameter("emaFast", DefaultValue = 12)]
        public int emaFast_parameter { get; set; }
        bool _emaFastGot;
        Mq4Double emaFast_backfield;
        Mq4Double emaFast
        {
            get
            {
                if (!_emaFastGot)
                    emaFast_backfield = emaFast_parameter;
                return emaFast_backfield;
            }
            set { emaFast_backfield = value; }
        }

        [Parameter("emaSlow", DefaultValue = 26)]
        public int emaSlow_parameter { get; set; }
        bool _emaSlowGot;
        Mq4Double emaSlow_backfield;
        Mq4Double emaSlow
        {
            get
            {
                if (!_emaSlowGot)
                    emaSlow_backfield = emaSlow_parameter;
                return emaSlow_backfield;
            }
            set { emaSlow_backfield = value; }
        }

        [Parameter("signalPeriod", DefaultValue = 9)]
        public int signalPeriod_parameter { get; set; }
        bool _signalPeriodGot;
        Mq4Double signalPeriod_backfield;
        Mq4Double signalPeriod
        {
            get
            {
                if (!_signalPeriodGot)
                    signalPeriod_backfield = signalPeriod_parameter;
                return signalPeriod_backfield;
            }
            set { signalPeriod_backfield = value; }
        }

        [Parameter("timeFrame", DefaultValue = 0)]
        public int timeFrame_parameter { get; set; }
        bool _timeFrameGot;
        Mq4Double timeFrame_backfield;
        Mq4Double timeFrame
        {
            get
            {
                if (!_timeFrameGot)
                    timeFrame_backfield = timeFrame_parameter;
                return timeFrame_backfield;
            }
            set { timeFrame_backfield = value; }
        }

        [Parameter("minFromPrice", DefaultValue = 0)]
        public int minFromPrice_parameter { get; set; }
        bool _minFromPriceGot;
        Mq4Double minFromPrice_backfield;
        Mq4Double minFromPrice
        {
            get
            {
                if (!_minFromPriceGot)
                    minFromPrice_backfield = minFromPrice_parameter;
                return minFromPrice_backfield;
            }
            set { minFromPrice_backfield = value; }
        }

        [Parameter("tradeForMinutes", DefaultValue = 0)]
        public int tradeForMinutes_parameter { get; set; }
        bool _tradeForMinutesGot;
        Mq4Double tradeForMinutes_backfield;
        Mq4Double tradeForMinutes
        {
            get
            {
                if (!_tradeForMinutesGot)
                    tradeForMinutes_backfield = tradeForMinutes_parameter;
                return tradeForMinutes_backfield;
            }
            set { tradeForMinutes_backfield = value; }
        }

        [Parameter("gridOffset", DefaultValue = 0)]
        public int gridOffset_parameter { get; set; }
        bool _gridOffsetGot;
        Mq4Double gridOffset_backfield;
        Mq4Double gridOffset
        {
            get
            {
                if (!_gridOffsetGot)
                    gridOffset_backfield = gridOffset_parameter;
                return gridOffset_backfield;
            }
            set { gridOffset_backfield = value; }
        }

        [Parameter("longGridCenter", DefaultValue = 0.0)]
        public double longGridCenter_parameter { get; set; }
        bool _longGridCenterGot;
        Mq4Double longGridCenter_backfield;
        Mq4Double longGridCenter
        {
            get
            {
                if (!_longGridCenterGot)
                    longGridCenter_backfield = longGridCenter_parameter;
                return longGridCenter_backfield;
            }
            set { longGridCenter_backfield = value; }
        }

        [Parameter("shortGridCenter", DefaultValue = 0.0)]
        public double shortGridCenter_parameter { get; set; }
        bool _shortGridCenterGot;
        Mq4Double shortGridCenter_backfield;
        Mq4Double shortGridCenter
        {
            get
            {
                if (!_shortGridCenterGot)
                    shortGridCenter_backfield = shortGridCenter_parameter;
                return shortGridCenter_backfield;
            }
            set { shortGridCenter_backfield = value; }
        }

        [Parameter("longGridLow", DefaultValue = 0.0)]
        public double longGridLow_parameter { get; set; }
        bool _longGridLowGot;
        Mq4Double longGridLow_backfield;
        Mq4Double longGridLow
        {
            get
            {
                if (!_longGridLowGot)
                    longGridLow_backfield = longGridLow_parameter;
                return longGridLow_backfield;
            }
            set { longGridLow_backfield = value; }
        }

        [Parameter("longGridHigh", DefaultValue = 0.0)]
        public double longGridHigh_parameter { get; set; }
        bool _longGridHighGot;
        Mq4Double longGridHigh_backfield;
        Mq4Double longGridHigh
        {
            get
            {
                if (!_longGridHighGot)
                    longGridHigh_backfield = longGridHigh_parameter;
                return longGridHigh_backfield;
            }
            set { longGridHigh_backfield = value; }
        }

        [Parameter("shortGridLow", DefaultValue = 0.0)]
        public double shortGridLow_parameter { get; set; }
        bool _shortGridLowGot;
        Mq4Double shortGridLow_backfield;
        Mq4Double shortGridLow
        {
            get
            {
                if (!_shortGridLowGot)
                    shortGridLow_backfield = shortGridLow_parameter;
                return shortGridLow_backfield;
            }
            set { shortGridLow_backfield = value; }
        }

        [Parameter("shortGridHigh", DefaultValue = 0.0)]
        public double shortGridHigh_parameter { get; set; }
        bool _shortGridHighGot;
        Mq4Double shortGridHigh_backfield;
        Mq4Double shortGridHigh
        {
            get
            {
                if (!_shortGridHighGot)
                    shortGridHigh_backfield = shortGridHigh_parameter;
                return shortGridHigh_backfield;
            }
            set { shortGridHigh_backfield = value; }
        }

        [Parameter("profitTarget", DefaultValue = 15000.0)]
        public double profitTarget_parameter { get; set; }
        bool _profitTargetGot;
        Mq4Double profitTarget_backfield;
        Mq4Double profitTarget
        {
            get
            {
                if (!_profitTargetGot)
                    profitTarget_backfield = profitTarget_parameter;
                return profitTarget_backfield;
            }
            set { profitTarget_backfield = value; }
        }

        [Parameter("suspendGrid", DefaultValue = FALSE)]
        public bool suspendGrid_parameter { get; set; }
        bool _suspendGridGot;
        Mq4Double suspendGrid_backfield;
        Mq4Double suspendGrid
        {
            get
            {
                if (!_suspendGridGot)
                    suspendGrid_backfield = suspendGrid_parameter;
                return suspendGrid_backfield;
            }
            set { suspendGrid_backfield = value; }
        }

        [Parameter("shutdownGrid", DefaultValue = FALSE)]
        public bool shutdownGrid_parameter { get; set; }
        bool _shutdownGridGot;
        Mq4Double shutdownGrid_backfield;
        Mq4Double shutdownGrid
        {
            get
            {
                if (!_shutdownGridGot)
                    shutdownGrid_backfield = shutdownGrid_parameter;
                return shutdownGrid_backfield;
            }
            set { shutdownGrid_backfield = value; }
        }





        Mq4Double gi_344 = FALSE;
        Mq4Double gi_340 = TRUE;
        Mq4Double g_count_336 = 0;
        Mq4Double g_count_332 = 0;
        Mq4Double gi_328 = FALSE;
        Mq4Double gd_320 = 0.0;
        Mq4Double gd_312 = 0.0;
        Mq4Double gd_304 = 0.0;
        Mq4Double gd_unused_296 = 0.0;
        Mq4Double g_datetime_288 = 0.0;
        Mq4String gs_grid_280 = "Grid";

        int indicator_buffers = 0;


        Mq4Double indicator_width1 = 1;
        Mq4Double indicator_width2 = 1;
        Mq4Double indicator_width3 = 1;
        Mq4Double indicator_width4 = 1;
        Mq4Double indicator_width5 = 1;
        Mq4Double indicator_width6 = 1;
        Mq4Double indicator_width7 = 1;
        Mq4Double indicator_width8 = 1;






        List<Mq4OutputDataSeries> AllBuffers = new List<Mq4OutputDataSeries>();
        public List<DataSeries> AllOutputDataSeries = new List<DataSeries>();

        protected override void OnStart()
        {


            CommonInitialize();


            try
            {
                Mq4Init();
            } catch (Exception e)
            {

            }
        }

        protected override void OnTick()
        {
            var index = MarketSeries.Close.Count - 1;


            try
            {
                Mq4Start();
            } catch (Exception e)
            {

            }
        }

        private bool IsLastBar
        {
            get { return true; }
        }

        int _currentIndex;
        CachedStandardIndicators _cachedStandardIndicators;
        Mq4ChartObjects _mq4ChartObjects;
        Mq4ArrayToDataSeriesConverterFactory _mq4ArrayToDataSeriesConverterFactory;
        Mq4MarketDataSeries Open;
        Mq4MarketDataSeries High;
        Mq4MarketDataSeries Low;
        Mq4MarketDataSeries Close;
        Mq4MarketDataSeries Median;
        Mq4MarketDataSeries Volume;
        Mq4TimeSeries Time;

        private void CommonInitialize()
        {
            Open = new Mq4MarketDataSeries(MarketSeries.Open);
            High = new Mq4MarketDataSeries(MarketSeries.High);
            Low = new Mq4MarketDataSeries(MarketSeries.Low);
            Close = new Mq4MarketDataSeries(MarketSeries.Close);
            Volume = new Mq4MarketDataSeries(MarketSeries.TickVolume);
            Median = new Mq4MarketDataSeries(MarketSeries.Median);
            Time = new Mq4TimeSeries(MarketSeries.OpenTime);

            _cachedStandardIndicators = new CachedStandardIndicators(Indicators);
            _mq4ChartObjects = new Mq4ChartObjects(ChartObjects, MarketSeries.OpenTime);
            _mq4ArrayToDataSeriesConverterFactory = new Mq4ArrayToDataSeriesConverterFactory(() => CreateDataSeries());
        }
        private int Bars
        {
            get { return MarketSeries.Close.Count; }
        }
        private int Digits
        {
            get
            {
                if (Symbol == null)
                    return 0;
                return Symbol.Digits;
            }
        }

        Mq4Double Point
        {
            get
            {
                if (Symbol == null)
                    return 1E-05;

                return Symbol.TickSize;
            }
        }
        private int Period()
        {
            if (TimeFrame == TimeFrame.Minute)
                return 1;
            if (TimeFrame == TimeFrame.Minute2)
                return 2;
            if (TimeFrame == TimeFrame.Minute3)
                return 3;
            if (TimeFrame == TimeFrame.Minute4)
                return 4;
            if (TimeFrame == TimeFrame.Minute5)
                return 5;
            if (TimeFrame == TimeFrame.Minute10)
                return 10;
            if (TimeFrame == TimeFrame.Minute15)
                return 15;
            if (TimeFrame == TimeFrame.Minute30)
                return 30;
            if (TimeFrame == TimeFrame.Hour)
                return 60;
            if (TimeFrame == TimeFrame.Hour4)
                return 240;
            if (TimeFrame == TimeFrame.Hour12)
                return 720;
            if (TimeFrame == TimeFrame.Daily)
                return 1440;
            if (TimeFrame == TimeFrame.Weekly)
                return 10080;

            return 43200;
        }

        public TimeFrame PeriodToTimeFrame(int period)
        {
            switch (period)
            {
                case 0:
                    return TimeFrame;
                case 1:
                    return TimeFrame.Minute;
                case 2:
                    return TimeFrame.Minute2;
                case 3:
                    return TimeFrame.Minute3;
                case 4:
                    return TimeFrame.Minute4;
                case 5:
                    return TimeFrame.Minute5;
                case 10:
                    return TimeFrame.Minute10;
                case 15:
                    return TimeFrame.Minute15;
                case 30:
                    return TimeFrame.Minute30;
                case 60:
                    return TimeFrame.Hour;
                case 240:
                    return TimeFrame.Hour4;
                case 720:
                    return TimeFrame.Hour12;
                case 1440:
                    return TimeFrame.Daily;
                case 10080:
                    return TimeFrame.Weekly;
                case 43200:
                    return TimeFrame.Monthly;
                default:
                    throw new NotSupportedException(string.Format("TimeFrame {0} minutes isn't supported by cAlgo", period));
            }
        }





        Mq4Double MathAbs(double x)
        {
            return Math.Abs(x);
        }



















        int StrToTime(Mq4String value)
        {
            var dateTime = StrToDateTime(value);
            return Mq4TimeSeries.ToInteger(dateTime);
        }

//{
        private static readonly Regex TimeRegex = new Regex("((?<year>\\d+)\\.(?<month>\\d+)\\.(?<day>\\d+)){0,1}\\s*((?<hour>\\d+)\\:(?<minute>\\d+)){0,1}", RegexOptions.Compiled);
        DateTime StrToDateTime(Mq4String value)
        {
            var dateTime = Server.Time.Date;

            var match = TimeRegex.Match(value);
            if (!match.Success)
                return dateTime;

            if (match.Groups["year"].Value != string.Empty)
            {
                dateTime = new DateTime(int.Parse(match.Groups["year"].Value), int.Parse(match.Groups["month"].Value), int.Parse(match.Groups["day"].Value));
            }
            if (match.Groups["hour"].Value != string.Empty)
                dateTime = dateTime.AddHours(int.Parse(match.Groups["hour"].Value));
            if (match.Groups["minute"].Value != string.Empty)
                dateTime = dateTime.AddMinutes(int.Parse(match.Groups["minute"].Value));

            return dateTime;
        }
        //}
        int ToMq4ErrorCode(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.BadVolume:
                    return ERR_INVALID_TRADE_VOLUME;
                case ErrorCode.NoMoney:
                    return ERR_NOT_ENOUGH_MONEY;
                case ErrorCode.MarketClosed:
                    return ERR_MARKET_CLOSED;
                case ErrorCode.Disconnected:
                    return ERR_NO_CONNECTION;
                case ErrorCode.Timeout:
                    return ERR_TRADE_TIMEOUT;
                default:
                    return ERR_COMMON_ERROR;
            }
        }






        Mq4String StringConcatenate(params object[] parameters)
        {
            var stringBuilder = new StringBuilder();
            foreach (var parameter in parameters)
                stringBuilder.Append(parameter.ToString());
            return stringBuilder.ToString();
        }














        int TimeCurrent()
        {
            return Mq4TimeSeries.ToInteger(Server.Time);
        }













        const string NotSupportedMaShift = "Converter supports only ma_shift = 0";










        int iTime(Mq4String symbol, int timeframe, int shift)
        {
            var timeSeries = GetSeries(symbol, timeframe).OpenTime;
            if (shift >= 0 && shift < timeSeries.Count)
                return Mq4TimeSeries.ToInteger(timeSeries.Last(shift));
            return 0;
        }



        Mq4Double MarketInfo(Mq4String symbol, int type)
        {
            var symbolObject = GetSymbol(symbol);
            switch (type)
            {
                case MODE_LOW:
                    return GetSeries(symbol, PERIOD_D1).Low.LastValue;
                case MODE_HIGH:
                    return GetSeries(symbol, PERIOD_D1).High.LastValue;
                case MODE_DIGITS:
                    return symbolObject.Digits;
                case MODE_TIME:
                    return TimeCurrent();
                case MODE_ASK:
                    return symbolObject.Ask;
                case MODE_BID:
                    return symbolObject.Bid;
                case MODE_SPREAD:
                    return symbolObject.Spread / symbolObject.TickSize;
                case MODE_PROFITCALCMODE:
                    return 0;
                case MODE_FREEZELEVEL:
                    return 0;
                case MODE_TRADEALLOWED:
                    return 1;
                case MODE_POINT:
                    return symbolObject.TickSize;
                case MODE_TICKSIZE:
                    return symbolObject.TickSize;
                case MODE_SWAPTYPE:
                    return 0;
                case MODE_MARGINCALCMODE:
                    return 0;
                case MODE_STOPLEVEL:
                    return symbolObject.TickSize;
                case MODE_MINLOT:
                    return symbolObject.ToLotsVolume(symbolObject.VolumeMin);
                case MODE_MAXLOT:
                    return symbolObject.ToLotsVolume(symbolObject.VolumeMax);
                case MODE_LOTSTEP:
                    return symbolObject.ToLotsVolume(symbolObject.VolumeStep);
                case MODE_TICKVALUE:
                    return symbolObject.TickValue;
                case MODE_LOTSIZE:
                    return symbolObject.ToNotNormalizedUnitsVolume(1);
                case MODE_MARGINREQUIRED:
                    return symbolObject.ToNotNormalizedUnitsVolume(1) / Account.Leverage * symbolObject.Ask * symbolObject.TickValue / symbolObject.TickSize;
            }
            return 0;
        }

        Mq4Double Bid
        {
            get
            {
                if (Symbol == null || double.IsNaN(Symbol.Bid))
                    return 0;
                return Symbol.Bid;
            }
        }

        Mq4Double Ask
        {
            get
            {
                if (Symbol == null || double.IsNaN(Symbol.Ask))
                    return 0;
                return Symbol.Ask;
            }
        }




        void Mq4Print(params object[] parameters)
        {
            Print(string.Join(string.Empty, parameters));
        }



















//{
        //AccessRights = AccessRights.FullAccess

        void Alert(params object[] objects)
        {
            if (IsBacktesting)
                return;

            var text = string.Join("", objects.Select(o => o.ToString()));
            _alertWindowWrapper.Value.ShowAlert(text);
            PlayAlertSoundIfNeeded();
        }

        Lazy<AlertWindowWrapper> _alertWindowWrapper = new Lazy<AlertWindowWrapper>(() => { return new AlertWindowWrapper(); });

        private const string _alertSound = "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";
        void PlayAlertSoundIfNeeded()
        {
            if (!IsLastBar || IsTesting() || (DateTime.Now - _lastPlayedTime) < TimeSpan.FromSeconds(1))
                return;

            _lastPlayedTime = DateTime.Now;
            _mediaPlayer = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationCoreAssembly, "System.Windows.Media.MediaPlayer");

            var soundFilePath = Path.Combine(FolderPaths._2calgoAppDataFolder, "alert_sound.mp3");
            if (!File.Exists(soundFilePath))
            {
                var binaryData = Convert.FromBase64String(_alertSound);
                File.WriteAllBytes(soundFilePath, binaryData);
            }
            var uri = new Uri(soundFilePath);

            ReflectionHelper.InvokeMethod(_mediaPlayer, "Open", uri);
            ReflectionHelper.InvokeMethod(_mediaPlayer, "Play");
        }

        private DateTime _lastPlayedTime;
        private object _mediaPlayer;

        public class AlertWindowWrapper : MarshalByRefObject
        {
            private static Thread _windowThread;
            private static readonly AutoResetEvent LoadedEvent = new AutoResetEvent(false);
            private static readonly object SyncObject = new object();

            private static object _window;
            private static AlertWindowModel _alertWindowModel;

            public void ShowAlert(string message)
            {
                lock (SyncObject)
                {
                    if (_window == null)
                        CreateWindow();

                    _alertWindowModel.Message = message;
                    var items = new List<AlertItem>(_alertWindowModel.Items);
                    items.Insert(0, new AlertItem(DateTime.Now, message));
                    _alertWindowModel.Items = items;
                }
            }

            private void CreateWindow()
            {
                _windowThread = new Thread(() =>
                {
                    try
                    {
                        _window = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.Window");
                        SubscribeToEvent(_window, "Loaded", "Window_Loaded");
                        SubscribeToEvent(_window, "Closing", "Window_Closing");
                        ReflectionHelper.SetProperty(_window, "ShowInTaskbar", true);
                        ReflectionHelper.SetProperty(_window, "ShowActivated", true);
                        ReflectionHelper.SetProperty(_window, "Width", 525);
                        ReflectionHelper.SetProperty(_window, "Height", 400);
                        ReflectionHelper.SetProperty(_window, "WindowStartupLocation", ReflectionHelper.GetStaticValue(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.WindowStartupLocation", "CenterScreen"));
                        ReflectionHelper.SetProperty(_window, "WindowStyle", ReflectionHelper.GetStaticValue(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.WindowStyle", "ToolWindow"));
                        ReflectionHelper.SetProperty(_window, "Title", "Alert");
                        var rootGrid = WpfReflectionHelper.CreateGrid();
                        WpfReflectionHelper.SetMargin(rootGrid, 5);
                        WpfReflectionHelper.AddAutoRowDefinition(rootGrid);
                        WpfReflectionHelper.AddStarRowDefinition(rootGrid);

                        var messageGrid = WpfReflectionHelper.CreateGrid();
                        WpfReflectionHelper.AddAutoColumnDefinition(messageGrid);
                        WpfReflectionHelper.AddStarColumnDefinition(messageGrid);

                        var image = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.Controls.Image");
                        WpfReflectionHelper.SetMargin(image, 20);
                        var bitmapImage = Base64ToImage("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");
                        ReflectionHelper.SetProperty(image, "Source", bitmapImage);
                        WpfReflectionHelper.AddChild(messageGrid, image);

                        var messageTextBlock = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.Controls.TextBlock");

                        WpfReflectionHelper.SetGridColumn(messageTextBlock, 1);
                        ReflectionHelper.SetProperty(messageTextBlock, "HorizontalAlignment", ReflectionHelper.GetEnumValue(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.HorizontalAlignment", "Left"));
                        ReflectionHelper.SetProperty(messageTextBlock, "TextWrapping", ReflectionHelper.GetEnumValue(WpfReflectionHelper.PresentationCoreAssembly, "System.Windows.TextWrapping", "Wrap"));
                        ReflectionHelper.SetProperty(messageTextBlock, "VerticalAlignment", ReflectionHelper.GetEnumValue(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.VerticalAlignment", "Center"));
                        ReflectionHelper.SetProperty(messageTextBlock, "FontSize", 18);
                        WpfReflectionHelper.SetBinding(messageTextBlock, "Message", "System.Windows.Controls.TextBlock", "TextProperty");
                        WpfReflectionHelper.AddChild(messageGrid, messageTextBlock);

                        var allAlertsGrid = WpfReflectionHelper.CreateGrid();
                        WpfReflectionHelper.SetGridRow(allAlertsGrid, 1);
                        WpfReflectionHelper.AddStarRowDefinition(allAlertsGrid);
                        WpfReflectionHelper.AddAutoRowDefinition(allAlertsGrid);
                        var dataGrid = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.Controls.DataGrid");
                        WpfReflectionHelper.SetBinding(dataGrid, "Items", "System.Windows.Controls.ItemsControl", "ItemsSourceProperty");
                        WpfReflectionHelper.AddChild(allAlertsGrid, dataGrid);
                        var closeButton = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationFrameworkAssembly, "System.Windows.Controls.Button");
                        SubscribeToEvent(closeButton, "Click", "ButtonOnClick");
                        WpfReflectionHelper.SetGridRow(closeButton, 1);
                        ReflectionHelper.SetProperty(closeButton, "Width", 100);
                        WpfReflectionHelper.SetMargin(closeButton, 0, 5, 0, 0);
                        ReflectionHelper.SetProperty(closeButton, "Content", "Close");
                        WpfReflectionHelper.AddChild(allAlertsGrid, closeButton);

                        WpfReflectionHelper.AddChild(rootGrid, messageGrid);
                        WpfReflectionHelper.AddChild(rootGrid, allAlertsGrid);

                        ReflectionHelper.SetProperty(_window, "Content", rootGrid);
                        _alertWindowModel = new AlertWindowModel();
                        ReflectionHelper.SetProperty(_window, "DataContext", _alertWindowModel);

                        ReflectionHelper.InvokeMethod(_window, "ShowDialog");
                    } catch (Exception exception)
                    {
                    }
                }) 
                {
                    IsBackground = false,
                    Name = "Alert Window thread"
                };

                _windowThread.TrySetApartmentState(ApartmentState.STA);
                _windowThread.Start();
                LoadedEvent.WaitOne();
            }

            private static object Base64ToImage(string stringValue)
            {

                var binaryData = Convert.FromBase64String(stringValue);

                var bitmapImage = ReflectionHelper.CreateInstance(WpfReflectionHelper.PresentationCoreAssembly, "System.Windows.Media.Imaging.BitmapImage");

                ReflectionHelper.InvokeMethod(bitmapImage, "BeginInit");
                ReflectionHelper.SetProperty(bitmapImage, "StreamSource", new MemoryStream(binaryData));
                ReflectionHelper.InvokeMethod(bitmapImage, "EndInit");

                return bitmapImage;
            }

            public void ButtonOnClick(object sender, object routedEventArgs)
            {
                ReflectionHelper.InvokeMethod(_window, "Close");
            }

            public void Window_Loaded(object s, object e)
            {
                LoadedEvent.Set();
            }

            public void Window_Closing(object sender, CancelEventArgs e)
            {
                _window = null;
                _alertWindowModel = null;
            }

            private void SubscribeToEvent(object @object, string eventName, string handlerName)
            {
                var eventInfo = @object.GetType().GetEvent(eventName);
                var methodInfo = GetType().GetMethod(handlerName);
                var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
                eventInfo.AddEventHandler(@object, handler);
            }
        }

        public class AlertWindowModel : INotifyPropertyChanged
        {
            private string _message;
            private IEnumerable<AlertItem> _items = new List<AlertItem>();

            public string Message
            {
                get { return _message; }
                set
                {
                    if (_message == value)
                        return;

                    _message = value;
                    OnPropertyChanged("Message");
                }
            }

            public IEnumerable<AlertItem> Items
            {
                get { return _items; }
                set
                {
                    if (_items == value)
                        return;

                    _items = value;
                    OnPropertyChanged("Items");
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class AlertItem
        {
            public DateTime Time { get; private set; }
            public string Message { get; private set; }

            public AlertItem(DateTime time, string message)
            {
                Time = time;
                Message = message;
            }
        }

        public static class WpfReflectionHelper
        {
            public static Assembly PresentationFrameworkAssembly = Assembly.Load("PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            public static Assembly PresentationCoreAssembly = Assembly.Load("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

            public static void SetMargin(object rootGrid, params object[] parameters)
            {
                var thickness = ReflectionHelper.CreateInstance(PresentationFrameworkAssembly, "System.Windows.Thickness", parameters);
                ReflectionHelper.SetProperty(rootGrid, "Margin", thickness);
            }

            public static object CreateGrid()
            {
                return ReflectionHelper.CreateInstance(PresentationFrameworkAssembly, "System.Windows.Controls.Grid");
            }

            public static void AddChild(object grid, object child)
            {
                var rootGridChildren = ReflectionHelper.GetPropertyValue(grid, "Children");
                ReflectionHelper.InvokeMethod(rootGridChildren, "Add", child);
            }

            private static readonly object AutoGridLegth = ReflectionHelper.GetStaticValue(PresentationFrameworkAssembly, "System.Windows.GridLength", "Auto");

            public static void AddAutoRowDefinition(object grid)
            {
                var rowDefinition = CreateRowDefinition();
                ReflectionHelper.SetProperty(rowDefinition, "Height", AutoGridLegth);
                AddRowDefinition(grid, rowDefinition);
            }

            public static void AddStarRowDefinition(object grid)
            {
                var rowDefinition = CreateRowDefinition();
                var starUnitType = ReflectionHelper.GetStaticValue(PresentationFrameworkAssembly, "System.Windows.GridUnitType", "Star");
                var gridLength = ReflectionHelper.CreateInstance(PresentationFrameworkAssembly, "System.Windows.GridLength", new[] 
                {
                    1,
                    starUnitType
                });
                ReflectionHelper.SetProperty(rowDefinition, "Height", gridLength);
                AddRowDefinition(grid, rowDefinition);
            }

            private static object CreateRowDefinition()
            {
                var rowDefinition = ReflectionHelper.CreateInstance(PresentationFrameworkAssembly, "System.Windows.Controls.RowDefinition");
                return rowDefinition;
            }

            public static void AddRowDefinition(object grid, object rowDefinition)
            {
                var rowDefinitions = ReflectionHelper.GetPropertyValue(grid, "RowDefinitions");
                ReflectionHelper.InvokeMethod(rowDefinitions, "Add", rowDefinition);
            }

            public static void AddAutoColumnDefinition(object grid)
            {
                var ColumnDefinition = CreateColumnDefinition();
                ReflectionHelper.SetProperty(ColumnDefinition, "Width", AutoGridLegth);
                AddColumnDefinition(grid, ColumnDefinition);
            }

            public static void AddStarColumnDefinition(object grid)
            {
                var ColumnDefinition = CreateColumnDefinition();
                var starUnitType = ReflectionHelper.GetStaticValue(PresentationFrameworkAssembly, "System.Windows.GridUnitType", "Star");
                var gridLength = ReflectionHelper.CreateInstance(PresentationFrameworkAssembly, "System.Windows.GridLength", new[] 
                {
                    1,
                    starUnitType
                });
                ReflectionHelper.SetProperty(ColumnDefinition, "Width", gridLength);
                AddColumnDefinition(grid, ColumnDefinition);
            }

            private static object CreateColumnDefinition()
            {
                var ColumnDefinition = ReflectionHelper.CreateInstance(PresentationFrameworkAssembly, "System.Windows.Controls.ColumnDefinition");
                return ColumnDefinition;
            }

            public static void AddColumnDefinition(object grid, object ColumnDefinition)
            {
                var ColumnDefinitions = ReflectionHelper.GetPropertyValue(grid, "ColumnDefinitions");
                ReflectionHelper.InvokeMethod(ColumnDefinitions, "Add", ColumnDefinition);
            }

            public static void SetGridColumn(object element, int column)
            {
                var columnProperty = ReflectionHelper.GetStaticValue(PresentationFrameworkAssembly, "System.Windows.Controls.Grid", "ColumnProperty");
                ReflectionHelper.InvokeMethod(element, "SetValue", columnProperty, column);
            }

            public static void SetGridRow(object element, int row)
            {
                var columnProperty = ReflectionHelper.GetStaticValue(PresentationFrameworkAssembly, "System.Windows.Controls.Grid", "RowProperty");
                ReflectionHelper.InvokeMethod(element, "SetValue", columnProperty, row);
            }

            public static void SetBinding(object element, string path, string propertyTypeName, string propertyName)
            {
                var property = ReflectionHelper.GetStaticValue(PresentationFrameworkAssembly, propertyTypeName, propertyName);
                ReflectionHelper.InvokeMethod(element, "SetBinding", property, path);
            }
        }

        public static class ReflectionHelper
        {
            public static object GetEnumValue(Assembly assembly, string typeName, string value)
            {
                var type = assembly.GetType(typeName);
                return Enum.Parse(type, value);
            }

            public static void InvokeMethod(object instance, string methodName, params object[] parameters)
            {
                instance.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, instance, parameters);
            }

            public static object InvokeStaticMethod(Assembly assembly, string typeName, string methodName, params object[] parameters)
            {
                return assembly.GetType(typeName).InvokeMember(methodName, BindingFlags.InvokeMethod, null, null, parameters);
            }

            public static object GetStaticValue(Assembly assembly, string typeName, string propertyName)
            {
                var type = assembly.GetType(typeName);
                var propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo != null)
                    return propertyInfo.GetValue(null, null);

                var fieldInfo = type.GetField(propertyName);
                return fieldInfo.GetValue(null);
            }

            public static void SetProperty(object instance, string propertyName, Assembly assembly, string typeName, params object[] parameters)
            {
                var propertyInfo = instance.GetType().GetProperty(propertyName);
                var propertyValue = CreateInstance(assembly, typeName, parameters);
                propertyInfo.SetValue(instance, propertyValue, null);
            }

            public static void SetProperty(object instance, string propertyName, object value)
            {
                var propertyInfo = instance.GetType().GetProperty(propertyName);
                propertyInfo.SetValue(instance, value, null);
            }

            public static object GetPropertyValue(object instance, string propertyName)
            {
                var propertyInfo = instance.GetType().GetProperty(propertyName);
                return propertyInfo.GetValue(instance, null);
            }

            public static object CreateInstance(Assembly assembly, string typeName)
            {
                return CreateInstance(assembly, typeName, new object[0]);
            }

            public static object CreateInstance(Assembly assembly, string typeName, object[] parameters)
            {
                var pointType = assembly.GetType(typeName);
                var propertyValue = Activator.CreateInstance(pointType, parameters);
                return propertyValue;
            }
        }
        //}

        private int _lastError;

        Mq4Double IsTesting()
        {
            return IsBacktesting;
        }












        const string GlobalVariablesPath = "Software\\2calgo\\Global Variables\\";






        Symbol GetSymbol(string symbolCode)
        {
            if (symbolCode == "0" || string.IsNullOrEmpty(symbolCode))
            {
                return Symbol;
            }
            return MarketData.GetSymbol(symbolCode);
        }

        MarketSeries GetSeries(string symbol, int period)
        {
            var timeFrame = PeriodToTimeFrame(period);
            var symbolObject = GetSymbol(symbol);

            if (symbolObject == Symbol && timeFrame == TimeFrame)
                return MarketSeries;

            return MarketData.GetSeries(symbolObject.Code, timeFrame);
        }

        private DataSeries ToAppliedPrice(string symbol, int timeframe, int constant)
        {
            var series = GetSeries(symbol, timeframe);
            switch (constant)
            {
                case PRICE_OPEN:
                    return series.Open;
                case PRICE_HIGH:
                    return series.High;
                case PRICE_LOW:
                    return series.Low;
                case PRICE_CLOSE:
                    return series.Close;
                case PRICE_MEDIAN:
                    return series.Median;
                case PRICE_TYPICAL:
                    return series.Typical;
                case PRICE_WEIGHTED:
                    return series.WeightedClose;
            }
            throw new NotImplementedException("Converter doesn't support working with this type of AppliedPrice");
        }
        const string xArrow = "✖";

        public static string GetArrowByCode(int code)
        {
            switch (code)
            {
                case 0:
                    return string.Empty;
                case 32:
                    return " ";
                case 33:
                    return "✏";
                case 34:
                    return "✂";
                case 35:
                    return "✁";
                case 40:
                    return "☎";
                case 41:
                    return "✆";
                case 42:
                    return "✉";
                case 54:
                    return "⌛";
                case 55:
                    return "⌨";
                case 62:
                    return "✇";
                case 63:
                    return "✍";
                case 65:
                    return "✌";
                case 69:
                    return "☜";
                case 70:
                    return "☞";
                case 71:
                    return "☝";
                case 72:
                    return "☟";
                case 74:
                    return "☺";
                case 76:
                    return "☹";
                case 78:
                    return "☠";
                case 79:
                    return "⚐";
                case 81:
                    return "✈";
                case 82:
                    return "☼";
                case 84:
                    return "❄";
                case 86:
                    return "✞";
                case 88:
                    return "✠";
                case 89:
                    return "✡";
                case 90:
                    return "☪";
                case 91:
                    return "☯";
                case 92:
                    return "ॐ";
                case 93:
                    return "☸";
                case 94:
                    return "♈";
                case 95:
                    return "♉";
                case 96:
                    return "♊";
                case 97:
                    return "♋";
                case 98:
                    return "♌";
                case 99:
                    return "♍";
                case 100:
                    return "♎";
                case 101:
                    return "♏";
                case 102:
                    return "♐";
                case 103:
                    return "♑";
                case 104:
                    return "♒";
                case 105:
                    return "♓";
                case 106:
                    return "&";
                case 107:
                    return "&";
                case 108:
                    return "●";
                case 109:
                    return "❍";
                case 110:
                    return "■";
                case 111:
                case 112:
                    return "□";
                case 113:
                    return "❑";
                case 114:
                    return "❒";
                case 115:
                case 116:
                    return "⧫";
                case 117:
                case 119:
                    return "◆";
                case 118:
                    return "❖";
                case 120:
                    return "⌧";
                case 121:
                    return "⍓";
                case 122:
                    return "⌘";
                case 123:
                    return "❀";
                case 124:
                    return "✿";
                case 125:
                    return "❝";
                case 126:
                    return "❞";
                case 127:
                    return "▯";
                case 128:
                    return "⓪";
                case 129:
                    return "①";
                case 130:
                    return "②";
                case 131:
                    return "③";
                case 132:
                    return "④";
                case 133:
                    return "⑤";
                case 134:
                    return "⑥";
                case 135:
                    return "⑦";
                case 136:
                    return "⑧";
                case 137:
                    return "⑨";
                case 138:
                    return "⑩";
                case 139:
                    return "⓿";
                case 140:
                    return "❶";
                case 141:
                    return "❷";
                case 142:
                    return "❸";
                case 143:
                    return "❹";
                case 144:
                    return "❺";
                case 145:
                    return "❻";
                case 146:
                    return "❼";
                case 147:
                    return "❽";
                case 148:
                    return "❾";
                case 149:
                    return "❿";
                case 158:
                    return "·";
                case 159:
                    return "•";
                case 160:
                case 166:
                    return "▪";
                case 161:
                    return "○";
                case 162:
                case 164:
                    return "⭕";
                case 165:
                    return "◎";
                case 167:
                    return "✖";
                case 168:
                    return "◻";
                case 170:
                    return "✦";
                case 171:
                    return "★";
                case 172:
                    return "✶";
                case 173:
                    return "✴";
                case 174:
                    return "✹";
                case 175:
                    return "✵";
                case 177:
                    return "⌖";
                case 178:
                    return "⟡";
                case 179:
                    return "⌑";
                case 181:
                    return "✪";
                case 182:
                    return "✰";
                case 195:
                case 197:
                case 215:
                case 219:
                case 223:
                case 231:
                    return "◀";
                case 196:
                case 198:
                case 224:
                    return "▶";
                case 213:
                    return "⌫";
                case 214:
                    return "⌦";
                case 216:
                    return "➢";
                case 220:
                    return "➲";
                case 232:
                    return "➔";
                case 233:
                case 199:
                case 200:
                case 217:
                case 221:
                case 225:
                    return "◭";
                case 234:
                case 201:
                case 202:
                case 218:
                case 222:
                case 226:
                    return "⧨";
                case 239:
                    return "⇦";
                case 240:
                    return "⇨";
                case 241:
                    return "◭";
                case 242:
                    return "⧨";
                case 243:
                    return "⬄";
                case 244:
                    return "⇳";
                case 245:
                case 227:
                case 235:
                    return "↖";
                case 246:
                case 228:
                case 236:
                    return "↗";
                case 247:
                case 229:
                case 237:
                    return "↙";
                case 248:
                case 230:
                case 238:
                    return "↘";
                case 249:
                    return "▭";
                case 250:
                    return "▫";
                case 251:
                    return "✗";
                case 252:
                    return "✓";
                case 253:
                    return "☒";
                case 254:
                    return "☑";
                default:
                    return xArrow;
            }
        }
        class Mq4OutputDataSeries : IMq4DoubleArray
        {
            public IndicatorDataSeries OutputDataSeries { get; private set; }
            private readonly IndicatorDataSeries _originalValues;
            private int _currentIndex;
            private int _shift;
            private double _emptyValue = EMPTY_VALUE;
            private readonly ChartObjects _chartObjects;
            private readonly int _style;
            private readonly int _bufferIndex;
            private readonly ConvertedRobot _indicator;

            public Mq4OutputDataSeries(ConvertedRobot indicator, IndicatorDataSeries outputDataSeries, ChartObjects chartObjects, int style, int bufferIndex, Func<IndicatorDataSeries> dataSeriesFactory, int lineWidth, Colors? color = null)
            {
                OutputDataSeries = outputDataSeries;
                _chartObjects = chartObjects;
                _style = style;
                _bufferIndex = bufferIndex;
                _indicator = indicator;
                Color = color;
                _originalValues = dataSeriesFactory();
                LineWidth = lineWidth;
            }

            public int LineWidth { get; private set; }
            public Colors? Color { get; private set; }

            public int Length
            {
                get { return OutputDataSeries.Count; }
            }

            public void Resize(int newSize)
            {
            }

            public void SetCurrentIndex(int index)
            {
                _currentIndex = index;
            }

            public void SetShift(int shift)
            {
                _shift = shift;
            }

            public void SetEmptyValue(double emptyValue)
            {
                _emptyValue = emptyValue;
            }

            public Mq4Double this[int index]
            {
                get
                {
                    var indexToGetFrom = _currentIndex - index + _shift;
                    if (indexToGetFrom < 0 || indexToGetFrom > _currentIndex)
                        return 0;
                    if (indexToGetFrom >= _originalValues.Count)
                        return _emptyValue;

                    return _originalValues[indexToGetFrom];
                }
                set
                {
                    var indexToSet = _currentIndex - index + _shift;
                    if (indexToSet < 0)
                        return;

                    _originalValues[indexToSet] = value;

                    var valueToSet = value;
                    if (valueToSet == _emptyValue)
                        valueToSet = double.NaN;

                    if (indexToSet < 0)
                        return;

                    OutputDataSeries[indexToSet] = valueToSet;

                    switch (_style)
                    {
                        case DRAW_ARROW:
                            var arrowName = GetArrowName(indexToSet);
                            if (double.IsNaN(valueToSet))
                                _chartObjects.RemoveObject(arrowName);
                            else
                            {
                                var color = Color.HasValue ? Color.Value : Colors.Red;
                                _chartObjects.DrawText(arrowName, _indicator.ArrowByIndex[_bufferIndex], indexToSet, valueToSet, VerticalAlignment.Center, HorizontalAlignment.Center, color);
                            }
                            break;
                        case DRAW_HISTOGRAM:
                            if (false)
                            {
                                var anotherLine = _indicator.AllBuffers.FirstOrDefault(b => b.LineWidth == LineWidth && b != this);
                                if (anotherLine != null)
                                {
                                    var name = GetNameOfHistogramLineOnChartWindow(indexToSet);
                                    Colors color;
                                    if (this[index] > anotherLine[index])
                                        color = Color ?? Colors.Green;
                                    else
                                        color = anotherLine.Color ?? Colors.Green;
                                    var lineWidth = LineWidth;
                                    if (lineWidth != 1 && lineWidth < 5)
                                        lineWidth = 5;

                                    _chartObjects.DrawLine(name, indexToSet, this[index], indexToSet, anotherLine[index], color, lineWidth);
                                }
                            }
                            break;
                    }
                }
            }

            private string GetNameOfHistogramLineOnChartWindow(int index)
            {
                return string.Format("Histogram on chart window {0} {1}", LineWidth, index);
            }

            private string GetArrowName(int index)
            {
                return string.Format("Arrow {0} {1}", GetHashCode(), index);
            }
        }
















        public Dictionary<int, string> ArrowByIndex = new Dictionary<int, string> 
        {
            {
                0,
                xArrow
            },
            {
                1,
                xArrow
            },
            {
                2,
                xArrow
            },
            {
                3,
                xArrow
            },
            {
                4,
                xArrow
            },
            {
                5,
                xArrow
            },
            {
                6,
                xArrow
            },
            {
                7,
                xArrow
            }
        };
        void SetIndexArrow(int index, int code)
        {
            ArrowByIndex[index] = GetArrowByCode(code);
        }

        private int _indicatorCounted;
        int FILE_READ = 1;
        int FILE_WRITE = 2;
//int FILE_BIN = 8;
        int FILE_CSV = 8;

        int SEEK_END = 2;

        class FileInfo
        {
            public int Mode { get; set; }
            public int Handle { get; set; }
            public char Separator { get; set; }
            public string FileName { get; set; }
            public List<string> PendingParts { get; set; }
            public StreamWriter StreamWriter { get; set; }
            public StreamReader StreamReader { get; set; }
        }

        private Dictionary<int, FileInfo> _openedFiles = new Dictionary<int, FileInfo>();
        private int _handleCounter = 1000;







        class FolderPaths
        {
            public static string _2calgoAppDataFolder
            {
                get
                {
                    var result = Path.Combine(SystemAppData, "2calgo");
                    if (!Directory.Exists(result))
                        Directory.CreateDirectory(result);
                    return result;
                }
            }

            public static string _2calgoDesktopFolder
            {
                get
                {
                    var result = Path.Combine(Desktop, "2calgo");
                    if (!Directory.Exists(result))
                        Directory.CreateDirectory(result);
                    return result;
                }
            }

            static string SystemAppData
            {
                get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
            }

            static string Desktop
            {
                get { return Environment.GetFolderPath(Environment.SpecialFolder.Desktop); }
            }
        }
        const int MODE_TRADES = 0;
        const int MODE_HISTORY = 1;
        const int SELECT_BY_POS = 0;
        const int SELECT_BY_TICKET = 1;

        T GetPropertyValue<T>(Func<Position, T> getFromPosition, Func<PendingOrder, T> getFromPendingOrder, Func<HistoricalTrade, T> getFromHistory)
        {
            if (_currentOrder == null)
                return default(T);

            return GetPropertyValue<T>(_currentOrder, getFromPosition, getFromPendingOrder, getFromHistory);
        }

        T GetPropertyValue<T>(object obj, Func<Position, T> getFromPosition, Func<PendingOrder, T> getFromPendingOrder, Func<HistoricalTrade, T> getFromHistory)
        {
            if (obj is Position)
                return getFromPosition((Position)obj);
            if (obj is PendingOrder)
                return getFromPendingOrder((PendingOrder)obj);

            return getFromHistory((HistoricalTrade)obj);
        }

        private Mq4Double GetTicket(object trade)
        {
            return new Mq4Double(GetPropertyValue<int>(trade, _ => _.Id, _ => _.Id, _ => _.ClosingDealId));
        }
        Mq4Double OrderTicket()
        {
            if (_currentOrder == null)
                return 0;

            return GetTicket(_currentOrder);
        }
        private int GetMagicNumber(string label)
        {
            int magicNumber;
            if (int.TryParse(label, out magicNumber))
                return magicNumber;

            return 0;
        }

        private int GetMagicNumber(object order)
        {
            var label = GetPropertyValue<string>(order, _ => _.Label, _ => _.Label, _ => _.Label);
            return GetMagicNumber(label);
        }
        Mq4Double OrderMagicNumber()
        {
            if (_currentOrder == null)
                return 0;

            return GetMagicNumber(_currentOrder);
        }

        Mq4String OrderComment()
        {
            if (_currentOrder == null)
                return string.Empty;

            return GetPropertyValue<string>(_currentOrder, _ => _.Comment, _ => _.Comment, _ => _.Comment);
        }

        Mq4Double OrdersTotal()
        {
            return Positions.Count + PendingOrders.Count;
        }


        Mq4Double OrdersHistoryTotal()
        {
            return History.Count;
        }
        object _currentOrder;
        bool OrderSelect(int index, int @select, int pool = MODE_TRADES)
        {
            _currentOrder = null;

            if (pool == MODE_TRADES)
            {
                var allOrders = Positions.OfType<object>().Concat(PendingOrders.OfType<object>()).ToArray();

                switch (@select)
                {
                    case SELECT_BY_POS:
                        if (index < 0 || index >= allOrders.Length)
                            return false;

                        _currentOrder = allOrders[index];
                        return true;
                    case SELECT_BY_TICKET:
                        _currentOrder = GetOrderByTicket(index);
                        return _currentOrder != null;
                }
            }
            if (pool == MODE_HISTORY)
            {
                switch (@select)
                {
                    case SELECT_BY_POS:
                        if (index < 0 || index >= History.Count)
                            return false;

                        _currentOrder = History[index];
                        return true;
                    case SELECT_BY_TICKET:
                        _currentOrder = History.FindLast(index.ToString());
                        return _currentOrder != null;
                }
            }

            return false;
        }
        double GetLots(object order)
        {
            var volume = GetPropertyValue<long>(order, _ => _.Volume, _ => _.Volume, _ => _.Volume);
            var symbolCode = GetPropertyValue<string>(order, _ => _.SymbolCode, _ => _.SymbolCode, _ => _.SymbolCode);
            var symbolObject = MarketData.GetSymbol(symbolCode);

            return symbolObject.ToLotsVolume(volume);
        }

        object GetOrderByTicket(int ticket)
        {
            var allOrders = Positions.OfType<object>().Concat(PendingOrders.OfType<object>()).ToArray();

            return allOrders.FirstOrDefault(_ => GetTicket(_) == ticket);
        }
        Mq4Double OrderLots()
        {
            if (_currentOrder == null)
                return 0;

            return GetLots(_currentOrder);
        }

        Mq4Double OrderType()
        {
            if (_currentOrder == null)
                return 0;

            var position = _currentOrder as Position;
            if (position != null)
            {
                return position.TradeType == TradeType.Buy ? OP_BUY : OP_SELL;
            }
            var pendingOrder = _currentOrder as PendingOrder;
            if (pendingOrder != null)
            {
                if (pendingOrder.OrderType == PendingOrderType.Limit)
                    return pendingOrder.TradeType == TradeType.Buy ? OP_BUYLIMIT : OP_SELLLIMIT;
                return pendingOrder.TradeType == TradeType.Buy ? OP_BUYSTOP : OP_SELLSTOP;
            }

            var historicalTrade = (HistoricalTrade)_currentOrder;

            return historicalTrade.TradeType == TradeType.Buy ? OP_BUY : OP_SELL;
        }

        Mq4String OrderSymbol()
        {
            return GetPropertyValue<string>(_ => _.SymbolCode, _ => _.SymbolCode, _ => _.SymbolCode);
        }
        double GetOpenPrice(object order)
        {
            return GetPropertyValue<double>(order, _ => _.EntryPrice, _ => _.TargetPrice, _ => _.EntryPrice);
        }
        Mq4Double OrderOpenPrice()
        {
            if (_currentOrder == null)
                return 0;

            return GetOpenPrice(_currentOrder);
        }


        private double GetStopLoss(object order)
        {
            var nullableValue = GetPropertyValue<double?>(order, _ => _.StopLoss, _ => _.StopLoss, _ => 0);
            return nullableValue ?? 0;
        }

        private double GetTakeProfit(object order)
        {
            var nullableValue = GetPropertyValue<double?>(order, _ => _.TakeProfit, _ => _.TakeProfit, _ => 0);
            return nullableValue ?? 0;
        }
        Mq4Double OrderStopLoss()
        {
            if (_currentOrder == null)
                return 0;
            return GetStopLoss(_currentOrder);
        }

        Mq4Double OrderTakeProfit()
        {
            if (_currentOrder == null)
                return 0;
            return GetTakeProfit(_currentOrder);
        }

        Mq4Double OrderProfit()
        {
            var position = _currentOrder as Position;
            if (position != null)
                return position.NetProfit;

            var historicalTrade = _currentOrder as HistoricalTrade;
            if (historicalTrade != null)
                return historicalTrade.NetProfit;

            return 0;
        }

        Mq4Double OrderOpenTime()
        {
            var position = _currentOrder as Position;
            if (position != null)
                return Mq4TimeSeries.ToInteger(position.EntryTime);

            var historicalTrade = _currentOrder as HistoricalTrade;
            if (historicalTrade != null)
                return Mq4TimeSeries.ToInteger(historicalTrade.EntryTime);

            return 0;
        }




















        class ParametersKey
        {
            private readonly object[] _parameters;

            public ParametersKey(params object[] parameters)
            {
                _parameters = parameters;
            }

            public override bool Equals(object obj)
            {
                var other = (ParametersKey)obj;
                for (var i = 0; i < _parameters.Length; i++)
                {
                    if (!_parameters[i].Equals(other._parameters[i]))
                        return false;
                }
                return true;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = 0;
                    foreach (var parameter in _parameters)
                    {
                        hashCode = (hashCode * 397) ^ parameter.GetHashCode();
                    }
                    return hashCode;
                }
            }
        }

        class Cache<TValue>
        {
            private Dictionary<ParametersKey, TValue> _dictionary = new Dictionary<ParametersKey, TValue>();

            public bool TryGetValue(out TValue value, params object[] parameters)
            {
                var key = new ParametersKey(parameters);
                return _dictionary.TryGetValue(key, out value);
            }

            public void Add(TValue value, params object[] parameters)
            {
                var key = new ParametersKey(parameters);
                _dictionary.Add(key, value);
            }
        }


        private static MovingAverageType ToMaType(int constant)
        {
            switch (constant)
            {
                case MODE_SMA:
                    return MovingAverageType.Simple;
                case MODE_EMA:
                    return MovingAverageType.Exponential;
                case MODE_LWMA:
                    return MovingAverageType.Weighted;
                default:
                    throw new ArgumentOutOfRangeException("Not supported moving average type");
            }
        }
//{
        #region iMA
        private double iMA(Mq4String symbol, Mq4Double timeframe, Mq4Double period, Mq4Double ma_shift, Mq4Double ma_method, Mq4Double applied_price, Mq4Double shift)
        {
            var series = ToAppliedPrice(symbol, timeframe, applied_price);

            return CalculateiMA(series, period, ma_method, shift + ma_shift);
        }

        private double iMAOnArray(Mq4DoubleArray mq4Array, Mq4Double total, Mq4Double period, Mq4Double ma_shift, Mq4Double ma_method, Mq4Double shift)
        {
            var dataSeries = _mq4ArrayToDataSeriesConverterFactory.Create(mq4Array);
            return CalculateiMA(dataSeries, period, ma_method, shift + ma_shift);
        }

        private double CalculateiMA(DataSeries dataSeries, int period, int ma_method, int shift)
        {
            if (ma_method == MODE_SMMA)
            {
                return CalculateWellesWilderSmoothing(dataSeries, period, shift);
            }

            var maType = ToMaType(ma_method);
            var indicator = _cachedStandardIndicators.MovingAverage(dataSeries, period, maType);

            return indicator.Result[dataSeries.Count - 1 - shift];
        }

        private double CalculateWellesWilderSmoothing(DataSeries dataSeries, int period, int shift)
        {
            var indicator = _cachedStandardIndicators.WellesWilderSmoothing(dataSeries, period);

            return indicator.Result[dataSeries.Count - 1 - shift];
        }
        #endregion
        //}





//{
        private Mq4Double iMACD(Mq4String symbol, Mq4Double timeframe, Mq4Double fast_ema_period, Mq4Double slow_ema_period, Mq4Double signal_period, Mq4Double applied_price, Mq4Double mode, Mq4Double shift)
        {
            var series = ToAppliedPrice(symbol, timeframe, applied_price);

            var indicator = Indicators.MacdCrossOver(series, fast_ema_period, slow_ema_period, signal_period);

            switch (mode)
            {
                case MODE_MAIN:
                    return indicator.MACD[series.Count - 1 - shift];
                default:
                    return indicator.Signal[series.Count - 1 - shift];
            }
        }
//}

//{
        private Mq4Double iOsMA(Mq4String symbol, Mq4Double timeframe, Mq4Double fast_ema_period, Mq4Double slow_ema_period, Mq4Double signal_period, Mq4Double applied_price, Mq4Double shift)
        {
            var series = ToAppliedPrice(symbol, timeframe, applied_price);

            var indicator = Indicators.MacdCrossOver(series, fast_ema_period, slow_ema_period, signal_period);

            return indicator.Histogram[series.Count - 1 - shift];
        }
//}




















        class CachedStandardIndicators
        {
            private readonly IIndicatorsAccessor _indicatorsAccessor;

            public CachedStandardIndicators(IIndicatorsAccessor indicatorsAccessor)
            {
                _indicatorsAccessor = indicatorsAccessor;
            }
//{
            #region iMA

            private Cache<MovingAverage> _movingAverages = new Cache<MovingAverage>();
            private Cache<WellesWilderSmoothing> _wellesWilderSmoothings = new Cache<WellesWilderSmoothing>();

            public MovingAverage MovingAverage(DataSeries source, int periods, MovingAverageType maType)
            {
                MovingAverage indicator;

                if (_movingAverages.TryGetValue(out indicator, maType, periods, source))
                    return indicator;

                indicator = _indicatorsAccessor.MovingAverage(source, periods, maType);
                _movingAverages.Add(indicator, maType, periods, source);

                return indicator;
            }

            public WellesWilderSmoothing WellesWilderSmoothing(DataSeries source, int periods)
            {
                WellesWilderSmoothing indicator;

                if (_wellesWilderSmoothings.TryGetValue(out indicator, periods, source))
                    return indicator;

                indicator = _indicatorsAccessor.WellesWilderSmoothing(source, periods);
                _wellesWilderSmoothings.Add(indicator, periods, source);

                return indicator;
            }
            #endregion
            //}

        }
        const bool True = true;
        const bool False = false;
        const bool TRUE = true;
        const bool FALSE = false;
        Mq4Null NULL;
        const int EMPTY = -1;
        const double EMPTY_VALUE = 2147483647;
        public const int WHOLE_ARRAY = 0;

        const int MODE_SMA = 0;
        //Simple moving average
        const int MODE_EMA = 1;
        //Exponential moving average,
        const int MODE_SMMA = 2;
        //Smoothed moving average,
        const int MODE_LWMA = 3;
        //Linear weighted moving average. 
        const int PRICE_CLOSE = 0;
        //Close price. 
        const int PRICE_OPEN = 1;
        //Open price. 
        const int PRICE_HIGH = 2;
        //High price. 
        const int PRICE_LOW = 3;
        //Low price. 
        const int PRICE_MEDIAN = 4;
        //Median price, (high+low)/2. 
        const int PRICE_TYPICAL = 5;
        //Typical price, (high+low+close)/3. 
        const int PRICE_WEIGHTED = 6;
        //Weighted close price, (high+low+close+close)/4. 
        const int DRAW_LINE = 0;
        const int DRAW_SECTION = 1;
        const int DRAW_HISTOGRAM = 2;
        const int DRAW_ARROW = 3;
        const int DRAW_ZIGZAG = 4;
        const int DRAW_NONE = 12;

        const int STYLE_SOLID = 0;
        const int STYLE_DASH = 1;
        const int STYLE_DOT = 2;
        const int STYLE_DASHDOT = 3;
        const int STYLE_DASHDOTDOT = 4;

        const int MODE_OPEN = 0;
        const int MODE_LOW = 1;
        const int MODE_HIGH = 2;
        const int MODE_CLOSE = 3;
        const int MODE_VOLUME = 4;
        const int MODE_TIME = 5;
        const int MODE_BID = 9;
        const int MODE_ASK = 10;
        const int MODE_POINT = 11;
        const int MODE_DIGITS = 12;
        const int MODE_SPREAD = 13;
        const int MODE_TRADEALLOWED = 22;
        const int MODE_PROFITCALCMODE = 27;
        const int MODE_MARGINCALCMODE = 28;
        const int MODE_SWAPTYPE = 26;
        const int MODE_TICKSIZE = 17;
        const int MODE_FREEZELEVEL = 33;
        const int MODE_STOPLEVEL = 14;
        const int MODE_LOTSIZE = 15;
        const int MODE_TICKVALUE = 16;
        /*const int MODE_SWAPLONG = 18;
const int MODE_SWAPSHORT = 19;
const int MODE_STARTING = 20;
const int MODE_EXPIRATION = 21;    
*/
        const int MODE_MINLOT = 23;
        const int MODE_LOTSTEP = 24;
        const int MODE_MAXLOT = 25;
        /*const int MODE_MARGININIT = 29;
const int MODE_MARGINMAINTENANCE = 30;
const int MODE_MARGINHEDGED = 31;*/
        const int MODE_MARGINREQUIRED = 32;

        const int OBJ_VLINE = 0;
        const int OBJ_HLINE = 1;
        const int OBJ_TREND = 2;
        const int OBJ_FIBO = 10;

        /*const int OBJ_TRENDBYANGLE = 3;
    const int OBJ_REGRESSION = 4;
    const int OBJ_CHANNEL = 5;
    const int OBJ_STDDEVCHANNEL = 6;
    const int OBJ_GANNLINE = 7;
    const int OBJ_GANNFAN = 8;
    const int OBJ_GANNGRID = 9;
    const int OBJ_FIBOTIMES = 11;
    const int OBJ_FIBOFAN = 12;
    const int OBJ_FIBOARC = 13;
    const int OBJ_EXPANSION = 14;
    const int OBJ_FIBOCHANNEL = 15;*/
        const int OBJ_RECTANGLE = 16;
        /*const int OBJ_TRIANGLE = 17;
    const int OBJ_ELLIPSE = 18;
    const int OBJ_PITCHFORK = 19;
    const int OBJ_CYCLES = 20;*/
        const int OBJ_TEXT = 21;
        const int OBJ_ARROW = 22;
        const int OBJ_LABEL = 23;

        const int OBJPROP_TIME1 = 0;
        const int OBJPROP_PRICE1 = 1;
        const int OBJPROP_TIME2 = 2;
        const int OBJPROP_PRICE2 = 3;
        const int OBJPROP_TIME3 = 4;
        const int OBJPROP_PRICE3 = 5;
        const int OBJPROP_COLOR = 6;
        const int OBJPROP_STYLE = 7;
        const int OBJPROP_WIDTH = 8;
        const int OBJPROP_BACK = 9;
        const int OBJPROP_RAY = 10;
        const int OBJPROP_ELLIPSE = 11;
        //const int OBJPROP_SCALE = 12;
        const int OBJPROP_ANGLE = 13;
        //angle for text rotation
        const int OBJPROP_ARROWCODE = 14;
        const int OBJPROP_TIMEFRAMES = 15;
        //const int OBJPROP_DEVIATION = 16;
        const int OBJPROP_FONTSIZE = 100;
        const int OBJPROP_CORNER = 101;
        const int OBJPROP_XDISTANCE = 102;
        const int OBJPROP_YDISTANCE = 103;
        const int OBJPROP_FIBOLEVELS = 200;
        const int OBJPROP_LEVELCOLOR = 201;
        const int OBJPROP_LEVELSTYLE = 202;
        const int OBJPROP_LEVELWIDTH = 203;
        const int OBJPROP_FIRSTLEVEL = 210;

        const int PERIOD_M1 = 1;
        const int PERIOD_M5 = 5;
        const int PERIOD_M15 = 15;
        const int PERIOD_M30 = 30;
        const int PERIOD_H1 = 60;
        const int PERIOD_H4 = 240;
        const int PERIOD_D1 = 1440;
        const int PERIOD_W1 = 10080;
        const int PERIOD_MN1 = 43200;

        const int TIME_DATE = 1;
        const int TIME_MINUTES = 2;
        const int TIME_SECONDS = 4;

        const int MODE_MAIN = 0;
        const int MODE_BASE = 0;
        const int MODE_PLUSDI = 1;
        const int MODE_MINUSDI = 2;
        const int MODE_SIGNAL = 1;

        const int MODE_UPPER = 1;
        const int MODE_LOWER = 2;

        const int MODE_GATORLIPS = 3;
        const int MODE_GATORJAW = 1;
        const int MODE_GATORTEETH = 2;

        const int CLR_NONE = 32768;

        const int White = 16777215;
        const int Snow = 16448255;
        const int MintCream = 16449525;
        const int LavenderBlush = 16118015;
        const int AliceBlue = 16775408;
        const int Honeydew = 15794160;
        const int Ivory = 15794175;
        const int Seashell = 15660543;
        const int WhiteSmoke = 16119285;
        const int OldLace = 15136253;
        const int MistyRose = 14804223;
        const int Lavender = 16443110;
        const int Linen = 15134970;
        const int LightCyan = 16777184;
        const int LightYellow = 14745599;
        const int Cornsilk = 14481663;
        const int PapayaWhip = 14020607;
        const int AntiqueWhite = 14150650;
        const int Beige = 14480885;
        const int LemonChiffon = 13499135;
        const int BlanchedAlmond = 13495295;
        const int LightGoldenrod = 13826810;
        const int Bisque = 12903679;
        const int Pink = 13353215;
        const int PeachPuff = 12180223;
        const int Gainsboro = 14474460;
        const int LightPink = 12695295;
        const int Moccasin = 11920639;
        const int NavajoWhite = 11394815;
        const int Wheat = 11788021;
        const int LightGray = 13882323;
        const int PaleTurquoise = 15658671;
        const int PaleGoldenrod = 11200750;
        const int PowderBlue = 15130800;
        const int Thistle = 14204888;
        const int PaleGreen = 10025880;
        const int LightBlue = 15128749;
        const int LightSteelBlue = 14599344;
        const int LightSkyBlue = 16436871;
        const int Silver = 12632256;
        const int Aquamarine = 13959039;
        const int LightGreen = 9498256;
        const int Khaki = 9234160;
        const int Plum = 14524637;
        const int LightSalmon = 8036607;
        const int SkyBlue = 15453831;
        const int LightCoral = 8421616;
        const int Violet = 15631086;
        const int Salmon = 7504122;
        const int HotPink = 11823615;
        const int BurlyWood = 8894686;
        const int DarkSalmon = 8034025;
        const int Tan = 9221330;
        const int MediumSlateBlue = 15624315;
        const int SandyBrown = 6333684;
        const int DarkGray = 11119017;
        const int CornflowerBlue = 15570276;
        const int Coral = 5275647;
        const int PaleVioletRed = 9662683;
        const int MediumPurple = 14381203;
        const int Orchid = 14053594;
        const int RosyBrown = 9408444;
        const int Tomato = 4678655;
        const int DarkSeaGreen = 9419919;
        const int Cyan = 16776960;
        const int MediumAquamarine = 11193702;
        const int GreenYellow = 3145645;
        const int MediumOrchid = 13850042;
        const int IndianRed = 6053069;
        const int DarkKhaki = 7059389;
        const int SlateBlue = 13458026;
        const int RoyalBlue = 14772545;
        const int Turquoise = 13688896;
        const int DodgerBlue = 16748574;
        const int MediumTurquoise = 13422920;
        const int DeepPink = 9639167;
        const int LightSlateGray = 10061943;
        const int BlueViolet = 14822282;
        const int Peru = 4163021;
        const int SlateGray = 9470064;
        const int Gray = 8421504;
        const int Red = 255;
        const int Magenta = 16711935;
        const int Blue = 16711680;
        const int DeepSkyBlue = 16760576;
        const int Aqua = 16776960;
        const int SpringGreen = 8388352;
        const int Lime = 65280;
        const int Chartreuse = 65407;
        const int Yellow = 65535;
        const int Gold = 55295;
        const int Orange = 42495;
        const int DarkOrange = 36095;
        const int OrangeRed = 17919;
        const int LimeGreen = 3329330;
        const int YellowGreen = 3329434;
        const int DarkOrchid = 13382297;
        const int CadetBlue = 10526303;
        const int LawnGreen = 64636;
        const int MediumSpringGreen = 10156544;
        const int Goldenrod = 2139610;
        const int SteelBlue = 11829830;
        const int Crimson = 3937500;
        const int Chocolate = 1993170;
        const int MediumSeaGreen = 7451452;
        const int MediumVioletRed = 8721863;
        const int FireBrick = 2237106;
        const int DarkViolet = 13828244;
        const int LightSeaGreen = 11186720;
        const int DimGray = 6908265;
        const int DarkTurquoise = 13749760;
        const int Brown = 2763429;
        const int MediumBlue = 13434880;
        const int Sienna = 2970272;
        const int DarkSlateBlue = 9125192;
        const int DarkGoldenrod = 755384;
        const int SeaGreen = 5737262;
        const int OliveDrab = 2330219;
        const int ForestGreen = 2263842;
        const int SaddleBrown = 1262987;
        const int DarkOliveGreen = 3107669;
        const int DarkBlue = 9109504;
        const int MidnightBlue = 7346457;
        const int Indigo = 8519755;
        const int Maroon = 128;
        const int Purple = 8388736;
        const int Navy = 8388608;
        const int Teal = 8421376;
        const int Green = 32768;
        const int Olive = 32896;
        const int DarkSlateGray = 5197615;
        const int DarkGreen = 25600;
        const int Fuchsia = 16711935;
        const int Black = 0;

        const int SYMBOL_LEFTPRICE = 5;
        const int SYMBOL_RIGHTPRICE = 6;

        const int SYMBOL_ARROWUP = 241;
        const int SYMBOL_ARROWDOWN = 242;
        const int SYMBOL_STOPSIGN = 251;
        /*
const int SYMBOL_THUMBSUP = 67;
const int SYMBOL_THUMBSDOWN = 68;	
const int SYMBOL_CHECKSIGN = 25;
*/

        public const int MODE_ASCEND = 1;
        public const int MODE_DESCEND = 2;

        const int MODE_TENKANSEN = 1;
        const int MODE_KIJUNSEN = 2;
        const int MODE_SENKOUSPANA = 3;
        const int MODE_SENKOUSPANB = 4;
        const int MODE_CHINKOUSPAN = 5;
        const int OP_BUY = 0;
        const int OP_SELL = 1;
        const int OP_BUYLIMIT = 2;
        const int OP_SELLLIMIT = 3;
        const int OP_BUYSTOP = 4;
        const int OP_SELLSTOP = 5;
        const int OBJ_PERIOD_M1 = 0x1;
        const int OBJ_PERIOD_M5 = 0x2;
        const int OBJ_PERIOD_M15 = 0x4;
        const int OBJ_PERIOD_M30 = 0x8;
        const int OBJ_PERIOD_H1 = 0x10;
        const int OBJ_PERIOD_H4 = 0x20;
        const int OBJ_PERIOD_D1 = 0x40;
        const int OBJ_PERIOD_W1 = 0x80;
        const int OBJ_PERIOD_MN1 = 0x100;
        const int OBJ_ALL_PERIODS = 0x1ff;

        const int REASON_REMOVE = 1;
        const int REASON_RECOMPILE = 2;
        const int REASON_CHARTCHANGE = 3;
        const int REASON_CHARTCLOSE = 4;
        const int REASON_PARAMETERS = 5;
        const int REASON_ACCOUNT = 6;
        const int ERR_NO_ERROR = 0;
        const int ERR_NO_RESULT = 1;
        const int ERR_COMMON_ERROR = 2;
        const int ERR_INVALID_TRADE_PARAMETERS = 3;
        const int ERR_SERVER_BUSY = 4;
        const int ERR_OLD_VERSION = 5;
        const int ERR_NO_CONNECTION = 6;
        const int ERR_NOT_ENOUGH_RIGHTS = 7;
        const int ERR_TOO_FREQUENT_REQUESTS = 8;
        const int ERR_MALFUNCTIONAL_TRADE = 9;
        const int ERR_ACCOUNT_DISABLED = 64;
        const int ERR_INVALID_ACCOUNT = 65;
        const int ERR_TRADE_TIMEOUT = 128;
        const int ERR_INVALID_PRICE = 129;
        const int ERR_INVALID_STOPS = 130;
        const int ERR_INVALID_TRADE_VOLUME = 131;
        const int ERR_MARKET_CLOSED = 132;
        const int ERR_TRADE_DISABLED = 133;
        const int ERR_NOT_ENOUGH_MONEY = 134;
        const int ERR_PRICE_CHANGED = 135;
        const int ERR_OFF_QUOTES = 136;
        const int ERR_BROKER_BUSY = 137;
        const int ERR_REQUOTE = 138;
        const int ERR_ORDER_LOCKED = 139;
        const int ERR_LONG_POSITIONS_ONLY_ALLOWED = 140;
        const int ERR_TOO_MANY_REQUESTS = 141;
        const int ERR_TRADE_MODIFY_DENIED = 145;
        const int ERR_TRADE_CONTEXT_BUSY = 146;
        const int ERR_TRADE_EXPIRATION_DENIED = 147;
        const int ERR_TRADE_TOO_MANY_ORDERS = 148;
        const int ERR_TRADE_HEDGE_PROHIBITED = 149;
        const int ERR_TRADE_PROHIBITED_BY_FIFO = 150;
        const int ERR_NO_MQLERROR = 4000;
        const int ERR_WRONG_FUNCTION_POINTER = 4001;
        const int ERR_ARRAY_INDEX_OUT_OF_RANGE = 4002;
        const int ERR_NO_MEMORY_FOR_CALL_STACK = 4003;
        const int ERR_RECURSIVE_STACK_OVERFLOW = 4004;
        const int ERR_NOT_ENOUGH_STACK_FOR_PARAM = 4005;
        const int ERR_NO_MEMORY_FOR_PARAM_STRING = 4006;
        const int ERR_NO_MEMORY_FOR_TEMP_STRING = 4007;
        const int ERR_NOT_INITIALIZED_STRING = 4008;
        const int ERR_NOT_INITIALIZED_ARRAYSTRING = 4009;
        const int ERR_NO_MEMORY_FOR_ARRAYSTRING = 4010;
        const int ERR_TOO_LONG_STRING = 4011;
        const int ERR_REMAINDER_FROM_ZERO_DIVIDE = 4012;
        const int ERR_ZERO_DIVIDE = 4013;
        const int ERR_UNKNOWN_COMMAND = 4014;
        const int ERR_WRONG_JUMP = 4015;
        const int ERR_NOT_INITIALIZED_ARRAY = 4016;
        const int ERR_DLL_CALLS_NOT_ALLOWED = 4017;
        const int ERR_CANNOT_LOAD_LIBRARY = 4018;
        const int ERR_CANNOT_CALL_FUNCTION = 4019;
        const int ERR_EXTERNAL_CALLS_NOT_ALLOWED = 4020;
        const int ERR_NO_MEMORY_FOR_RETURNED_STR = 4021;
        const int ERR_SYSTEM_BUSY = 4022;
        const int ERR_INVALID_FUNCTION_PARAMSCNT = 4050;
        const int ERR_INVALID_FUNCTION_PARAMVALUE = 4051;
        const int ERR_STRING_FUNCTION_INTERNAL = 4052;
        const int ERR_SOME_ARRAY_ERROR = 4053;
        const int ERR_INCORRECT_SERIESARRAY_USING = 4054;
        const int ERR_CUSTOM_INDICATOR_ERROR = 4055;
        const int ERR_INCOMPATIBLE_ARRAYS = 4056;
        const int ERR_GLOBAL_VARIABLES_PROCESSING = 4057;
        const int ERR_GLOBAL_VARIABLE_NOT_FOUND = 4058;
        const int ERR_FUNC_NOT_ALLOWED_IN_TESTING = 4059;
        const int ERR_FUNCTION_NOT_CONFIRMED = 4060;
        const int ERR_SEND_MAIL_ERROR = 4061;
        const int ERR_STRING_PARAMETER_EXPECTED = 4062;
        const int ERR_INTEGER_PARAMETER_EXPECTED = 4063;
        const int ERR_DOUBLE_PARAMETER_EXPECTED = 4064;
        const int ERR_ARRAY_AS_PARAMETER_EXPECTED = 4065;
        const int ERR_HISTORY_WILL_UPDATED = 4066;
        const int ERR_TRADE_ERROR = 4067;
        const int ERR_END_OF_FILE = 4099;
        const int ERR_SOME_FILE_ERROR = 4100;
        const int ERR_WRONG_FILE_NAME = 4101;
        const int ERR_TOO_MANY_OPENED_FILES = 4102;
        const int ERR_CANNOT_OPEN_FILE = 4103;
        const int ERR_INCOMPATIBLE_FILEACCESS = 4104;
        const int ERR_NO_ORDER_SELECTED = 4105;
        const int ERR_UNKNOWN_SYMBOL = 4106;
        const int ERR_INVALID_PRICE_PARAM = 4107;
        const int ERR_INVALID_TICKET = 4108;
        const int ERR_TRADE_NOT_ALLOWED = 4109;
        const int ERR_LONGS_NOT_ALLOWED = 4110;
        const int ERR_SHORTS_NOT_ALLOWED = 4111;
        const int ERR_OBJECT_ALREADY_EXISTS = 4200;
        const int ERR_UNKNOWN_OBJECT_PROPERTY = 4201;
        const int ERR_OBJECT_DOES_NOT_EXIST = 4202;
        const int ERR_UNKNOWN_OBJECT_TYPE = 4203;
        const int ERR_NO_OBJECT_NAME = 4204;
        const int ERR_OBJECT_COORDINATES_ERROR = 4205;
        const int ERR_NO_SPECIFIED_SUBWINDOW = 4206;
        const int ERR_SOME_OBJECT_ERROR = 4207;
        class Mq4ChartObjects
        {
            private readonly ChartObjects _algoChartObjects;
            private readonly TimeSeries _timeSeries;

            private readonly Dictionary<string, Mq4Object> _mq4ObjectByName = new Dictionary<string, Mq4Object>();
            private readonly List<string> _mq4ObjectNameByIndex = new List<string>();

            public Mq4ChartObjects(ChartObjects chartObjects, TimeSeries timeSeries)
            {
                _algoChartObjects = chartObjects;
                _timeSeries = timeSeries;
            }

            public void Set(string name, int index, Mq4Double value)
            {
                if (!_mq4ObjectByName.ContainsKey(name))
                    return;
                _mq4ObjectByName[name].Set(index, value);
                _mq4ObjectByName[name].Draw();
            }
            public void SetText(string name, string text, int font_size, string font, int color)
            {
                if (!_mq4ObjectByName.ContainsKey(name))
                    return;

                Set(name, OBJPROP_COLOR, color);
            }







            private T GetObject<T>(string name) where T : Mq4Object
            {
                Mq4Object mq4Object;
                if (!_mq4ObjectByName.TryGetValue(name, out mq4Object))
                    return null;
                return mq4Object as T;
            }

        }

        abstract class Mq4Object : IDisposable
        {
            private readonly ChartObjects _chartObjects;

            protected Mq4Object(string name, int type, ChartObjects chartObjects)
            {
                Name = name;
                Type = type;
                _chartObjects = chartObjects;
            }

            public int Type { get; private set; }

            public string Name { get; private set; }

            protected DateTime Time1
            {
                get
                {
                    int seconds = Get(OBJPROP_TIME1);
                    return Mq4TimeSeries.ToDateTime(seconds);
                }
            }

            protected double Price1
            {
                get { return Get(OBJPROP_PRICE1); }
            }

            protected DateTime Time2
            {
                get
                {
                    int seconds = Get(OBJPROP_TIME2);
                    return Mq4TimeSeries.ToDateTime(seconds);
                }
            }

            protected double Price2
            {
                get { return Get(OBJPROP_PRICE2); }
            }

            protected Colors Color
            {
                get
                {
                    int intColor = Get(OBJPROP_COLOR);
                    if (intColor != CLR_NONE)
                        return Mq4Colors.GetColorByInteger(intColor);

                    return Colors.Yellow;
                }
            }

            protected int Width
            {
                get { return Get(OBJPROP_WIDTH); }
            }

            protected int Style
            {
                get { return Get(OBJPROP_STYLE); }
            }

            public abstract void Draw();

            private readonly Dictionary<int, Mq4Double> _properties = new Dictionary<int, Mq4Double> 
            {
                {
                    OBJPROP_WIDTH,
                    new Mq4Double(1)
                },
                {
                    OBJPROP_COLOR,
                    new Mq4Double(CLR_NONE)
                },
                {
                    OBJPROP_RAY,
                    new Mq4Double(1)
                },

                {
                    OBJPROP_LEVELCOLOR,
                    new Mq4Double(CLR_NONE)
                },
                {
                    OBJPROP_LEVELSTYLE,
                    new Mq4Double(0)
                },
                {
                    OBJPROP_LEVELWIDTH,
                    new Mq4Double(1)
                },
                {
                    OBJPROP_FIBOLEVELS,
                    new Mq4Double(9)
                },
                {
                    OBJPROP_FIRSTLEVEL + 0,
                    new Mq4Double(0)
                },
                {
                    OBJPROP_FIRSTLEVEL + 1,
                    new Mq4Double(0.236)
                },
                {
                    OBJPROP_FIRSTLEVEL + 2,
                    new Mq4Double(0.382)
                },
                {
                    OBJPROP_FIRSTLEVEL + 3,
                    new Mq4Double(0.5)
                },
                {
                    OBJPROP_FIRSTLEVEL + 4,
                    new Mq4Double(0.618)
                },
                {
                    OBJPROP_FIRSTLEVEL + 5,
                    new Mq4Double(1)
                },
                {
                    OBJPROP_FIRSTLEVEL + 6,
                    new Mq4Double(1.618)
                },
                {
                    OBJPROP_FIRSTLEVEL + 7,
                    new Mq4Double(2.618)
                },
                {
                    OBJPROP_FIRSTLEVEL + 8,
                    new Mq4Double(4.236)
                }
            };

            public virtual void Set(int index, Mq4Double value)
            {
                _properties[index] = value;
            }

            public Mq4Double Get(int index)
            {
                return _properties.ContainsKey(index) ? _properties[index] : new Mq4Double(0);
            }

            private readonly List<string> _addedAlgoChartObjects = new List<string>();

            protected void DrawText(string objectName, string text, int index, double yValue, VerticalAlignment verticalAlignment = VerticalAlignment.Center, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center, Colors? color = null)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawText(objectName, text, index, yValue, verticalAlignment, horizontalAlignment, color);
            }

            protected void DrawText(string objectName, string text, StaticPosition position, Colors? color = null)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawText(objectName, text, position, color);
            }

            protected void DrawLine(string objectName, int index1, double y1, int index2, double y2, Colors color, double thickness = 1.0, cAlgo.API.LineStyle style = cAlgo.API.LineStyle.Solid)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawLine(objectName, index1, y1, index2, y2, color, thickness, style);
            }

            protected void DrawLine(string objectName, DateTime date1, double y1, DateTime date2, double y2, Colors color, double thickness = 1.0, cAlgo.API.LineStyle style = cAlgo.API.LineStyle.Solid)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawLine(objectName, date1, y1, date2, y2, color, thickness, style);
            }

            protected void DrawVerticalLine(string objectName, DateTime date, Colors color, double thickness = 1.0, cAlgo.API.LineStyle style = cAlgo.API.LineStyle.Solid)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawVerticalLine(objectName, date, color, thickness, style);
            }

            protected void DrawVerticalLine(string objectName, int index, Colors color, double thickness = 1.0, cAlgo.API.LineStyle style = cAlgo.API.LineStyle.Solid)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawVerticalLine(objectName, index, color, thickness, style);
            }

            protected void DrawHorizontalLine(string objectName, double y, Colors color, double thickness = 1.0, cAlgo.API.LineStyle style = cAlgo.API.LineStyle.Solid)
            {
                _addedAlgoChartObjects.Add(objectName);
                _chartObjects.DrawHorizontalLine(objectName, y, color, thickness, style);
            }

            public void Dispose()
            {
                foreach (var name in _addedAlgoChartObjects)
                {
                    _chartObjects.RemoveObject(name);
                }
            }
        }





        class Mq4Arrow : Mq4Object
        {
            private readonly TimeSeries _timeSeries;
            private int _index;

            public Mq4Arrow(string name, int type, ChartObjects chartObjects, TimeSeries timeSeries) : base(name, type, chartObjects)
            {
                _timeSeries = timeSeries;
            }

            public override void Set(int index, Mq4Double value)
            {
                base.Set(index, value);
                switch (index)
                {
                    case OBJPROP_TIME1:
                        _index = _timeSeries.GetIndexByTime(Time1);
                        break;
                }
            }

            private int ArrowCode
            {
                get { return Get(OBJPROP_ARROWCODE); }
            }

            public override void Draw()
            {
                string arrowString;
                HorizontalAlignment horizontalAlignment;
                switch (ArrowCode)
                {
                    case SYMBOL_RIGHTPRICE:
                        horizontalAlignment = HorizontalAlignment.Right;
                        arrowString = Price1.ToString();
                        break;
                    case SYMBOL_LEFTPRICE:
                        horizontalAlignment = HorizontalAlignment.Left;
                        arrowString = Price1.ToString();
                        break;
                    default:
                        arrowString = ConvertedRobot.GetArrowByCode(ArrowCode);
                        horizontalAlignment = HorizontalAlignment.Center;
                        break;
                }
                DrawText(Name, arrowString, _index, Price1, VerticalAlignment.Center, horizontalAlignment, Color);
            }
        }















        Mq4Double OrderSend(Mq4String symbol, int cmd, Mq4Double volume, Mq4Double price, Mq4Double slippagePoints, Mq4Double stoploss, Mq4Double takeprofit, Mq4String comment = null, Mq4Double? magic = null, int expiration = 0,
        int arrow_color = CLR_NONE)
        {
            _lastError = ERR_NO_ERROR;
            if (magic == null)
                magic = 0;
            var label = magic.Value.ToString();

            var symbolObject = GetSymbol(symbol);
            var volumeInUnits = symbolObject.ToUnitsVolume(volume);

            switch (cmd)
            {
                case OP_BUY:
                case OP_SELL:
                    {
                        var tradeType = cmd == OP_BUY ? TradeType.Buy : TradeType.Sell;
                        var slippageInPrice = symbolObject.TickSize * slippagePoints;
                        var slippageInPips = (int)Math.Round((double)slippageInPrice / symbolObject.PipSize);
                        double? stopLossPips = null;
                        if (stoploss != 0)
                            stopLossPips = tradeType == TradeType.Buy ? (symbolObject.Ask - stoploss) / symbolObject.PipSize : (stoploss - symbolObject.Bid) / symbolObject.PipSize;
                        double? takeProfitPips = null;
                        if (takeprofit != 0)
                            takeProfitPips = tradeType == TradeType.Buy ? (takeprofit - symbolObject.Ask) / symbolObject.PipSize : (symbolObject.Bid - takeprofit) / symbolObject.PipSize;

                        var marketOrderResult = ExecuteMarketOrder(tradeType, symbolObject, volumeInUnits, label, stopLossPips, takeProfitPips, slippageInPips, comment);

                        if (marketOrderResult.IsSuccessful)
                            return GetTicket(marketOrderResult.Position);
                        else
                        {
                            _lastError = ToMq4ErrorCode(marketOrderResult.Error.Value);
                            return -1;
                        }
                    }
                case OP_BUYLIMIT:
                case OP_SELLLIMIT:
                case OP_BUYSTOP:
                case OP_SELLSTOP:
                    {
                        var tradeType = cmd == OP_BUYLIMIT || cmd == OP_BUYSTOP ? TradeType.Buy : TradeType.Sell;

                        double? stopLossPips = null;
                        if (stoploss != 0)
                            stopLossPips = tradeType == TradeType.Buy ? (price - stoploss) / symbolObject.PipSize : (stoploss - price) / symbolObject.PipSize;
                        double? takeProfitPips = null;
                        if (takeprofit != 0)
                            takeProfitPips = tradeType == TradeType.Buy ? (takeprofit - price) / symbolObject.PipSize : (price - takeprofit) / symbolObject.PipSize;

                        TradeResult placeOrderResult;
                        if (cmd == OP_BUYLIMIT || cmd == OP_SELLLIMIT)
                            placeOrderResult = PlaceLimitOrder(tradeType, symbolObject, volumeInUnits, price, label, stopLossPips, takeProfitPips, expiration.ToNullableDateTime(), comment);
                        else
                            placeOrderResult = PlaceStopOrder(tradeType, symbolObject, volumeInUnits, price, label, stopLossPips, takeProfitPips, expiration.ToNullableDateTime(), comment);

                        if (placeOrderResult.IsSuccessful)
                            return GetTicket(placeOrderResult.PendingOrder);
                        else
                        {
                            _lastError = ToMq4ErrorCode(placeOrderResult.Error.Value);
                            return -1;
                        }
                    }
                default:
                    throw new Exception("Not supported by converter");
            }

            return 0;
        }
        Mq4Double OrderClose(int ticket, double lots, double price, int slippagePoints, int Color = CLR_NONE)
        {
            _lastError = ERR_NO_ERROR;

            var position = GetOrderByTicket(ticket) as Position;
            if (position == null)
            {
                _lastError = ERR_INVALID_TICKET;
                return false;
            }
            var symbolObject = MarketData.GetSymbol(position.SymbolCode);

            var volumeInUnits = symbolObject.ToUnitsVolume(lots);
            ClosePosition(position, volumeInUnits);

            if (!LastResult.IsSuccessful)
                _lastError = ToMq4ErrorCode(LastResult.Error.Value);

            return LastResult.IsSuccessful;
        }

        Mq4Double OrderModify(int ticket, double price, double stoploss, double takeprofit, int expiration, int arrow_color = CLR_NONE)
        {
            _lastError = ERR_NO_ERROR;

            var order = GetOrderByTicket(ticket);
            if (GetTakeProfit(order) == takeprofit && GetStopLoss(order) == stoploss && GetOpenPrice(order) == price)
            {
                _lastError = ERR_NO_RESULT;
                return false;
            }

            var position = order as Position;
            if (position != null)
            {
                ModifyPosition(position, stoploss.ToNullableDouble(), takeprofit.ToNullableDouble());
                if (!LastResult.IsSuccessful)
                    _lastError = ToMq4ErrorCode(LastResult.Error.Value);

                return LastResult.IsSuccessful;
            }

            var pendingOrder = (PendingOrder)order;
            var expirationTime = expiration.ToNullableDateTime();
            ModifyPendingOrder(pendingOrder, price, stoploss.ToNullableDouble(), takeprofit.ToNullableDouble(), expirationTime);

            if (!LastResult.IsSuccessful)
                _lastError = ToMq4ErrorCode(LastResult.Error.Value);

            return LastResult.IsSuccessful;
        }

        bool OrderDelete(int ticket, int Color = CLR_NONE)
        {
            _lastError = ERR_NO_ERROR;

            var pendingOrder = GetOrderByTicket(ticket) as PendingOrder;
            if (pendingOrder == null)
                return false;

            CancelPendingOrder(pendingOrder);

            if (!LastResult.IsSuccessful)
                _lastError = ToMq4ErrorCode(LastResult.Error.Value);

            return LastResult.IsSuccessful;
        }

    }

    //Custom Indicators Place Holder

    class Mq4DoubleComparer : IComparer<Mq4Double>
    {
        public int Compare(Mq4Double x, Mq4Double y)
        {
            return x.CompareTo(y);
        }
    }
    class Mq4String
    {
        private readonly string _value;

        public Mq4String(string value)
        {
            _value = value;
        }

        public static implicit operator Mq4String(string value)
        {
            return new Mq4String(value);
        }

        public static implicit operator Mq4String(int value)
        {
            return new Mq4String(value.ToString());
        }

        public static implicit operator Mq4String(Mq4Null mq4Null)
        {
            return new Mq4String(null);
        }

        public static implicit operator string(Mq4String mq4String)
        {
            if ((object)mq4String == null)
                return null;

            return mq4String._value;
        }

        public static implicit operator Mq4String(Mq4Double mq4Double)
        {
            return new Mq4String(mq4Double.ToString());
        }

        public static bool operator <(Mq4String x, Mq4String y)
        {
            return string.Compare(x._value, y._value) == -1;
        }

        public static bool operator >(Mq4String x, Mq4String y)
        {
            return string.Compare(x._value, y._value) == 1;
        }

        public static bool operator <(Mq4String x, string y)
        {
            return string.Compare(x._value, y) == -1;
        }

        public static bool operator >(Mq4String x, string y)
        {
            return string.Compare(x._value, y) == 1;
        }
        public static bool operator <=(Mq4String x, Mq4String y)
        {
            return string.Compare(x._value, y._value) <= 0;
        }

        public static bool operator >=(Mq4String x, Mq4String y)
        {
            return string.Compare(x._value, y._value) >= 0;
        }

        public static bool operator <=(Mq4String x, string y)
        {
            return string.Compare(x._value, y) <= 0;
        }

        public static bool operator >=(Mq4String x, string y)
        {
            return string.Compare(x._value, y) >= 0;
        }

        public static bool operator ==(Mq4String x, Mq4String y)
        {
            return string.Compare(x._value, y._value) == 0;
        }

        public static bool operator !=(Mq4String x, Mq4String y)
        {
            return string.Compare(x._value, y._value) != 0;
        }

        public static bool operator ==(Mq4String x, string y)
        {
            return string.Compare(x._value, y) == 0;
        }

        public static bool operator !=(Mq4String x, string y)
        {
            return string.Compare(x._value, y) != 0;
        }

        public override string ToString()
        {
            if ((object)this == null)
                return string.Empty;

            return _value.ToString();
        }

        public static readonly Mq4String Empty = new Mq4String(string.Empty);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Mq4String)obj);
        }

        protected bool Equals(Mq4String other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return (_value != null ? _value.GetHashCode() : 0);
        }
    }
    struct Mq4Char
    {
        char _char;

        public Mq4Char(byte code)
        {
            _char = Encoding.Unicode.GetString(new byte[] 
            {
                code,
                0
            })[0];
        }

        public Mq4Char(char @char)
        {
            _char = @char;
        }

        public static implicit operator char(Mq4Char mq4Char)
        {
            return mq4Char._char;
        }

        public static implicit operator Mq4Char(int code)
        {
            return new Mq4Char((byte)code);
        }

        public static implicit operator Mq4Char(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length == 0)
                return new Mq4Char(' ');
            return new Mq4Char(str[0]);
        }
    }
    struct Mq4Null
    {
        public static implicit operator string(Mq4Null mq4Null)
        {
            return (string)null;
        }

        public static implicit operator int(Mq4Null mq4Null)
        {
            return 0;
        }

        public static implicit operator double(Mq4Null mq4Null)
        {
            return 0;
        }
    }
    static class Comparers
    {
        public static IComparer<T> GetComparer<T>()
        {
            if (typeof(T) == typeof(Mq4Double))
                return (IComparer<T>)new Mq4DoubleComparer();

            return Comparer<T>.Default;
        }
    }
    static class DataSeriesExtensions
    {
        public static int InvertIndex(this DataSeries dataSeries, int index)
        {
            return dataSeries.Count - 1 - index;
        }

        public static Mq4Double Last(this DataSeries dataSeries, int shift, DataSeries sourceDataSeries)
        {
            return dataSeries[sourceDataSeries.Count - 1 - shift];
        }
    }
    static class TimeSeriesExtensions
    {
        public static DateTime Last(this TimeSeries timeSeries, int index)
        {
            return timeSeries[timeSeries.InvertIndex(index)];
        }

        public static int InvertIndex(this TimeSeries timeSeries, int index)
        {
            return timeSeries.Count - 1 - index;
        }

        public static int GetIndexByTime(this TimeSeries timeSeries, DateTime time)
        {
            var index = timeSeries.Count - 1;
            for (var i = timeSeries.Count - 1; i >= 0; i--)
            {
                if (timeSeries[i] < time)
                {
                    index = i + 1;
                    break;
                }
            }
            return index;
        }
    }
    static class Mq4Colors
    {
        public static Colors GetColorByInteger(int integer)
        {
            switch (integer)
            {
                case 16777215:
                    return Colors.White;
                case 16448255:
                    return Colors.Snow;
                case 16449525:
                    return Colors.MintCream;
                case 16118015:
                    return Colors.LavenderBlush;
                case 16775408:
                    return Colors.AliceBlue;
                case 15794160:
                    return Colors.Honeydew;
                case 15794175:
                    return Colors.Ivory;
                case 16119285:
                    return Colors.WhiteSmoke;
                case 15136253:
                    return Colors.OldLace;
                case 14804223:
                    return Colors.MistyRose;
                case 16443110:
                    return Colors.Lavender;
                case 15134970:
                    return Colors.Linen;
                case 16777184:
                    return Colors.LightCyan;
                case 14745599:
                    return Colors.LightYellow;
                case 14481663:
                    return Colors.Cornsilk;
                case 14020607:
                    return Colors.PapayaWhip;
                case 14150650:
                    return Colors.AntiqueWhite;
                case 14480885:
                    return Colors.Beige;
                case 13499135:
                    return Colors.LemonChiffon;
                case 13495295:
                    return Colors.BlanchedAlmond;
                case 12903679:
                    return Colors.Bisque;
                case 13353215:
                    return Colors.Pink;
                case 12180223:
                    return Colors.PeachPuff;
                case 14474460:
                    return Colors.Gainsboro;
                case 12695295:
                    return Colors.LightPink;
                case 11920639:
                    return Colors.Moccasin;
                case 11394815:
                    return Colors.NavajoWhite;
                case 11788021:
                    return Colors.Wheat;
                case 13882323:
                    return Colors.LightGray;
                case 15658671:
                    return Colors.PaleTurquoise;
                case 11200750:
                    return Colors.PaleGoldenrod;
                case 15130800:
                    return Colors.PowderBlue;
                case 14204888:
                    return Colors.Thistle;
                case 10025880:
                    return Colors.PaleGreen;
                case 15128749:
                    return Colors.LightBlue;
                case 14599344:
                    return Colors.LightSteelBlue;
                case 16436871:
                    return Colors.LightSkyBlue;
                case 12632256:
                    return Colors.Silver;
                case 13959039:
                    return Colors.Aquamarine;
                case 9498256:
                    return Colors.LightGreen;
                case 9234160:
                    return Colors.Khaki;
                case 14524637:
                    return Colors.Plum;
                case 8036607:
                    return Colors.LightSalmon;
                case 15453831:
                    return Colors.SkyBlue;
                case 8421616:
                    return Colors.LightCoral;
                case 15631086:
                    return Colors.Violet;
                case 7504122:
                    return Colors.Salmon;
                case 11823615:
                    return Colors.HotPink;
                case 8894686:
                    return Colors.BurlyWood;
                case 8034025:
                    return Colors.DarkSalmon;
                case 9221330:
                    return Colors.Tan;
                case 15624315:
                    return Colors.MediumSlateBlue;
                case 6333684:
                    return Colors.SandyBrown;
                case 11119017:
                    return Colors.DarkGray;
                case 15570276:
                    return Colors.CornflowerBlue;
                case 5275647:
                    return Colors.Coral;
                case 9662683:
                    return Colors.PaleVioletRed;
                case 14381203:
                    return Colors.MediumPurple;
                case 14053594:
                    return Colors.Orchid;
                case 9408444:
                    return Colors.RosyBrown;
                case 4678655:
                    return Colors.Tomato;
                case 9419919:
                    return Colors.DarkSeaGreen;
                case 11193702:
                    return Colors.MediumAquamarine;
                case 3145645:
                    return Colors.GreenYellow;
                case 13850042:
                    return Colors.MediumOrchid;
                case 6053069:
                    return Colors.IndianRed;
                case 7059389:
                    return Colors.DarkKhaki;
                case 13458026:
                    return Colors.SlateBlue;
                case 14772545:
                    return Colors.RoyalBlue;
                case 13688896:
                    return Colors.Turquoise;
                case 16748574:
                    return Colors.DodgerBlue;
                case 13422920:
                    return Colors.MediumTurquoise;
                case 9639167:
                    return Colors.DeepPink;
                case 10061943:
                    return Colors.LightSlateGray;
                case 14822282:
                    return Colors.BlueViolet;
                case 4163021:
                    return Colors.Peru;
                case 9470064:
                    return Colors.SlateGray;
                case 8421504:
                    return Colors.Gray;
                case 255:
                    return Colors.Red;
                case 16711935:
                    return Colors.Magenta;
                case 16711680:
                    return Colors.Blue;
                case 16760576:
                    return Colors.DeepSkyBlue;
                case 16776960:
                    return Colors.Aqua;
                case 8388352:
                    return Colors.SpringGreen;
                case 65280:
                    return Colors.Lime;
                case 65407:
                    return Colors.Chartreuse;
                case 65535:
                    return Colors.Yellow;
                case 55295:
                    return Colors.Gold;
                case 42495:
                    return Colors.Orange;
                case 36095:
                    return Colors.DarkOrange;
                case 17919:
                    return Colors.OrangeRed;
                case 3329330:
                    return Colors.LimeGreen;
                case 3329434:
                    return Colors.YellowGreen;
                case 13382297:
                    return Colors.DarkOrchid;
                case 10526303:
                    return Colors.CadetBlue;
                case 64636:
                    return Colors.LawnGreen;
                case 10156544:
                    return Colors.MediumSpringGreen;
                case 2139610:
                    return Colors.Goldenrod;
                case 11829830:
                    return Colors.SteelBlue;
                case 3937500:
                    return Colors.Crimson;
                case 1993170:
                    return Colors.Chocolate;
                case 7451452:
                    return Colors.MediumSeaGreen;
                case 8721863:
                    return Colors.MediumVioletRed;
                case 13828244:
                    return Colors.DarkViolet;
                case 11186720:
                    return Colors.LightSeaGreen;
                case 6908265:
                    return Colors.DimGray;
                case 13749760:
                    return Colors.DarkTurquoise;
                case 2763429:
                    return Colors.Brown;
                case 13434880:
                    return Colors.MediumBlue;
                case 2970272:
                    return Colors.Sienna;
                case 9125192:
                    return Colors.DarkSlateBlue;
                case 755384:
                    return Colors.DarkGoldenrod;
                case 5737262:
                    return Colors.SeaGreen;
                case 2330219:
                    return Colors.OliveDrab;
                case 2263842:
                    return Colors.ForestGreen;
                case 1262987:
                    return Colors.SaddleBrown;
                case 3107669:
                    return Colors.DarkOliveGreen;
                case 9109504:
                    return Colors.DarkBlue;
                case 7346457:
                    return Colors.MidnightBlue;
                case 8519755:
                    return Colors.Indigo;
                case 128:
                    return Colors.Maroon;
                case 8388736:
                    return Colors.Purple;
                case 8388608:
                    return Colors.Navy;
                case 8421376:
                    return Colors.Teal;
                case 32768:
                    return Colors.Green;
                case 32896:
                    return Colors.Olive;
                case 5197615:
                    return Colors.DarkSlateGray;
                case 25600:
                    return Colors.DarkGreen;
                case 0:
                default:
                    return Colors.Black;
            }
        }
    }
    static class EventExtensions
    {
        public static void Raise<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action != null)
                action(arg1, arg2);
        }
    }
    static class Mq4LineStyles
    {
        public static LineStyle ToLineStyle(int style)
        {
            switch (style)
            {
                case 1:
                    return LineStyle.Lines;
                case 2:
                    return LineStyle.Dots;
                case 3:
                case 4:
                    return LineStyle.LinesDots;
                default:
                    return LineStyle.Solid;
            }
        }
    }
    class Mq4TimeSeries
    {
        private readonly TimeSeries _timeSeries;
        private static readonly DateTime StartDateTime = new DateTime(1970, 1, 1);

        public Mq4TimeSeries(TimeSeries timeSeries)
        {
            _timeSeries = timeSeries;
        }

        public static int ToInteger(DateTime dateTime)
        {
            return (int)(dateTime - StartDateTime).TotalSeconds;
        }

        public static DateTime ToDateTime(int seconds)
        {
            return StartDateTime.AddSeconds(seconds);
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _timeSeries.Count)
                    return 0;

                DateTime dateTime = _timeSeries[_timeSeries.Count - 1 - index];

                return ToInteger(dateTime);
            }
        }
    }
    static class ConvertExtensions
    {
        public static double? ToNullableDouble(this double protection)
        {
            if (protection == 0)
                return null;
            return protection;
        }

        public static DateTime? ToNullableDateTime(this int time)
        {
            if (time == 0)
                return null;

            return Mq4TimeSeries.ToDateTime(time);
        }

        public static long ToUnitsVolume(this Symbol symbol, double lots)
        {
            return symbol.NormalizeVolume(symbol.ToNotNormalizedUnitsVolume(lots));
        }

        public static double ToNotNormalizedUnitsVolume(this Symbol symbol, double lots)
        {
            if (symbol.Code.Contains("XAU") || symbol.Code.Contains("XAG"))
                return 100 * lots;

            return 100000 * lots;
        }

        public static double ToLotsVolume(this Symbol symbol, long volume)
        {
            if (symbol.Code.Contains("XAU") || symbol.Code.Contains("XAG"))
                return volume * 1.0 / 100;

            return volume * 1.0 / 100000;
        }
    }
    struct Mq4Double : IComparable, IComparable<Mq4Double>
    {
        private readonly double _value;

        public Mq4Double(double value)
        {
            _value = value;
        }

        public static implicit operator double(Mq4Double property)
        {
            return property._value;
        }

        public static implicit operator int(Mq4Double property)
        {
            return (int)property._value;
        }

        public static implicit operator bool(Mq4Double property)
        {
            return (int)property._value != 0;
        }

        public static implicit operator Mq4Double(double value)
        {
            return new Mq4Double(value);
        }

        public static implicit operator Mq4Double(int value)
        {
            return new Mq4Double(value);
        }

        public static implicit operator Mq4Double(bool value)
        {
            return new Mq4Double(value ? 1 : 0);
        }

        public static implicit operator Mq4Double(Mq4Null value)
        {
            return new Mq4Double(0);
        }

        public static Mq4Double operator +(Mq4Double d1, Mq4Double d2)
        {
            return new Mq4Double(d1._value + d2._value);
        }

        public static Mq4Double operator -(Mq4Double d1, Mq4Double d2)
        {
            return new Mq4Double(d1._value - d2._value);
        }

        public static Mq4Double operator -(Mq4Double d)
        {
            return new Mq4Double(-d._value);
        }

        public static Mq4Double operator +(Mq4Double d)
        {
            return new Mq4Double(+d._value);
        }

        public static Mq4Double operator *(Mq4Double d1, Mq4Double d2)
        {
            return new Mq4Double(d1._value * d2._value);
        }

        public static Mq4Double operator /(Mq4Double d1, Mq4Double d2)
        {
            return new Mq4Double(d1._value / d2._value);
        }

        public static bool operator ==(Mq4Double d1, Mq4Double d2)
        {
            return d1._value == d2._value;
        }

        public static bool operator >(Mq4Double d1, Mq4Double d2)
        {
            return d1._value > d2._value;
        }

        public static bool operator >=(Mq4Double d1, Mq4Double d2)
        {
            return d1._value >= d2._value;
        }

        public static bool operator <(Mq4Double d1, Mq4Double d2)
        {
            return d1._value < d2._value;
        }

        public static bool operator <=(Mq4Double d1, Mq4Double d2)
        {
            return d1._value <= d2._value;
        }

        public static bool operator !=(Mq4Double d1, Mq4Double d2)
        {
            return d1._value != d2._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public int CompareTo(object obj)
        {
            return _value.CompareTo(obj);
        }

        public int CompareTo(Mq4Double obj)
        {
            return _value.CompareTo(obj);
        }
    }
    class Mq4DoubleTwoDimensionalArray
    {
        private List<Mq4Double> _data = new List<Mq4Double>();
        private List<Mq4DoubleArray> _arrays = new List<Mq4DoubleArray>();
        private readonly Mq4Double _defaultValue;
        private readonly int _size2;

        public Mq4DoubleTwoDimensionalArray(int size2)
        {
            _defaultValue = 0;
            _size2 = size2;
        }

        public void Add(Mq4Double value)
        {
            _data.Add(value);
        }

        private void EnsureCountIsEnough(int index)
        {
            while (_arrays.Count <= index)
                _arrays.Add(new Mq4DoubleArray());
        }

        public void Initialize(Mq4Double value)
        {
            for (var i = 0; i < _data.Count; i++)
                _data[i] = value;
        }

        public int Range(int index)
        {
            if (index == 0)
                return _data.Count;
            return this[0].Length;
        }

        public Mq4DoubleArray this[int index]
        {
            get
            {
                if (index < 0)
                    return new Mq4DoubleArray();

                EnsureCountIsEnough(index);

                return _arrays[index];
            }
        }

        public Mq4Double this[int index1, int index2]
        {
            get
            {
                if (index1 < 0)
                    return 0;

                EnsureCountIsEnough(index1);

                return _arrays[index1][index2];
            }
            set
            {
                if (index1 < 0)
                    return;

                EnsureCountIsEnough(index1);

                _arrays[index1][index2] = value;
            }
        }
    }
    class Mq4DoubleArray : IMq4DoubleArray, IEnumerable
    {
        private List<Mq4Double> _data = new List<Mq4Double>();
        private readonly Mq4Double _defaultValue;

        public Mq4DoubleArray(int size = 0)
        {
            _defaultValue = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        private bool _isInverted;
        public bool IsInverted
        {
            get { return _isInverted; }
            set { _isInverted = value; }
        }

        public void Add(Mq4Double value)
        {
            _data.Add(value);
        }

        private void EnsureCountIsEnough(int index)
        {
            while (_data.Count <= index)
                _data.Add(_defaultValue);
        }

        public int Length
        {
            get { return _data.Count; }
        }

        public void Resize(int newSize)
        {
            while (newSize < _data.Count)
                _data.RemoveAt(_data.Count - 1);

            while (newSize > _data.Count)
                _data.Add(_defaultValue);
        }

        public Mq4Double this[int index]
        {
            get
            {
                if (index < 0)
                    return _defaultValue;

                EnsureCountIsEnough(index);

                return _data[index];
            }
            set
            {
                if (index < 0)
                    return;

                EnsureCountIsEnough(index);

                _data[index] = value;
                Changed.Raise(index, value);
            }
        }
        public event Action<int, Mq4Double> Changed;
    }
    class Mq4MarketDataSeries : IMq4DoubleArray
    {
        private DataSeries _dataSeries;

        public Mq4MarketDataSeries(DataSeries dataSeries)
        {
            _dataSeries = dataSeries;
        }

        public Mq4Double this[int index]
        {
            get { return _dataSeries.Last(index); }
            set { }
        }

        public int Length
        {
            get { return _dataSeries.Count; }
        }

        public void Resize(int newSize)
        {
        }
    }
    class Mq4StringArray : IEnumerable
    {
        private List<Mq4String> _data = new List<Mq4String>();
        private readonly Mq4String _defaultValue;

        public Mq4StringArray(int size = 0)
        {
            _defaultValue = "";
        }

        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        private bool _isInverted;
        public bool IsInverted
        {
            get { return _isInverted; }
            set { _isInverted = value; }
        }

        public void Add(Mq4String value)
        {
            _data.Add(value);
        }

        private void EnsureCountIsEnough(int index)
        {
            while (_data.Count <= index)
                _data.Add(_defaultValue);
        }

        public int Length
        {
            get { return _data.Count; }
        }

        public void Resize(int newSize)
        {
            while (newSize < _data.Count)
                _data.RemoveAt(_data.Count - 1);

            while (newSize > _data.Count)
                _data.Add(_defaultValue);
        }

        public Mq4String this[int index]
        {
            get
            {
                if (index < 0)
                    return _defaultValue;

                EnsureCountIsEnough(index);

                return _data[index];
            }
            set
            {
                if (index < 0)
                    return;

                EnsureCountIsEnough(index);

                _data[index] = value;
            }
        }
    }
    interface IMq4DoubleArray
    {
        Mq4Double this[int index] { get; set; }
        int Length { get; }
        void Resize(int newSize);
    }
    class Mq4ArrayToDataSeriesConverter
    {
        private readonly Mq4DoubleArray _mq4Array;
        private readonly IndicatorDataSeries _dataSeries;

        public Mq4ArrayToDataSeriesConverter(Mq4DoubleArray mq4Array, IndicatorDataSeries dataSeries)
        {
            _mq4Array = mq4Array;
            _dataSeries = dataSeries;
            _mq4Array.Changed += OnValueChanged;
            CopyAllValues();
        }

        private void CopyAllValues()
        {
            for (var i = 0; i < _mq4Array.Length; i++)
            {
                if (_mq4Array.IsInverted)
                    _dataSeries[_mq4Array.Length - i] = _mq4Array[i];
                else
                    _dataSeries[i] = _mq4Array[i];
            }
        }

        private void OnValueChanged(int index, Mq4Double value)
        {
            int indexToSet;
            if (_mq4Array.IsInverted)
                indexToSet = _mq4Array.Length - index;
            else
                indexToSet = index;

            if (indexToSet < 0)
                return;

            _dataSeries[indexToSet] = value;
        }
    }
    class Mq4ArrayToDataSeriesConverterFactory
    {
        private readonly Dictionary<Mq4DoubleArray, IndicatorDataSeries> _cachedAdapters = new Dictionary<Mq4DoubleArray, IndicatorDataSeries>();
        private Func<IndicatorDataSeries> _dataSeriesFactory;

        public Mq4ArrayToDataSeriesConverterFactory(Func<IndicatorDataSeries> dataSeriesFactory)
        {
            _dataSeriesFactory = dataSeriesFactory;
        }

        public DataSeries Create(Mq4DoubleArray mq4Array)
        {
            IndicatorDataSeries dataSeries;

            if (_cachedAdapters.TryGetValue(mq4Array, out dataSeries))
                return dataSeries;

            dataSeries = _dataSeriesFactory();
            new Mq4ArrayToDataSeriesConverter(mq4Array, dataSeries);
            _cachedAdapters[mq4Array] = dataSeries;

            return dataSeries;
        }
    }

}
