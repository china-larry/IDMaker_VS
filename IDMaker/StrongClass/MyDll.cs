using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
/*
*********************************************************************************************
*  Author   :  周张奎 zhou zhang kui 3860 
*  DateTime :  2010-06-29
*  Version  :  1.0
*  Modify   :  unknown
*  tel      :  18688483860  15974748  gejiemei520@163.com
*  Function :  万孚荧光测试系统
* (C)Copyright 2010.06, 保留所有权益 (Build10.06.28)
*********************************************************************************************
*/
namespace IDMaker
{
    class MyDll
    {
        [DllImport("\\FitEqu.dll",
        EntryPoint = "GetSum",
        CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.Winapi)]
        public static extern int sum(int r, int b);

        //曲线拟合
        [DllImport("\\FitEqu.dll",
        EntryPoint = "FitEqu",
        CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.Winapi)]
        public static extern int FitEquCpp(double[] mx,double[] my,int iPower, int iPower2, int iMaxPower,bool mSubFlag,bool mLogFlag,int L,int sP, double[] a0,double[] a1,int[] n);//后面3个都是输出参数,n为两曲线项数


        //C#拟合
        public static int FitEqu(double[] mx, double[] my, int iPower, int iPower2, int iMaxPower, bool mSubFlag, bool mLogFlag, int L, int sp, double[] a0, double[] a1, int[] n)//后面3个都是输出参数,n为两曲线项数
        {
            int i = 0, j = 0, k = 0;
            //double mEx;
            int mp = 0;
            int mq = 0;
            double rss = 0, Orss = 0; //Orss = 10; modi by zhou zhang kui 2011-05-18
            double[] mX0 = new double[16];
            double[] mX1 = new double[16];
            double[] mY0 = new double[16];
            double[] mY1 = new double[16];
            double[,] ma0 = new double[6, 10];
            double[,] ma1 = new double[6, 10];
            double y0 = 0;
            n = new int[2];

            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 10; j++)
                {
                    ma0[i, j] = 0;
                    ma1[i, j] = 0;
                }
            }
            if (mSubFlag)	//如果分段
            {
                if (sp < 3)//分段点位置4到10之间
                    return 0;
                mp = sp;
                for (i = 0; i < 16; i++)//拷贝两段数据
                {
                    if (i < sp)
                    {
                        mX0[i] = mx[i];
                        mY0[i] = my[i];
                    }
                    else
                    {
                        mX1[i - sp] = mx[i - 1];
                        mY1[i - sp] = my[i - 1];
                    }
                }
                if (mLogFlag)	//如果取对数
                {
                    y0 = mY0[0];
                    for (i = 0; i < mp; i++)//第一段
                    {
                        if (mX0[i] <= 0)
                        {
                            for (j = i; j < mp; j++)//移掉前面一段的0
                            {
                                mX0[j] = mX0[j + 1];
                                mY0[j] = mY0[j + 1];
                            }
                            mp--;
                            i -= 1;//i减1再增1
                            continue;
                        }
                        mX0[i] = Math.Log10(mX0[i]);//+0.7;
                    }
                    for (i = 0; i < L - sp + 1; i++)//第二段
                    {
                        y0 = mX1[i] = Math.Log10(mX1[i]);//+0.7;
                    }
                    mq = i;
                }
                else
                {
                    mq = L - sp + 1;
                }
            }
            else//如果没有分段
            {
                for (i = 0; i < L; i++)
                {
                    if (my[i] == 0)
                        break;
                    mX0[i] = mx[i];
                    mY0[i] = my[i];
                }
                mp = i;
                if (mLogFlag)	//如果取对数
                {
                    y0 = mY0[0];
                    for (i = 0; i < mp; i++)
                    {
                        if (mX0[i] <= 0)
                        {
                            for (j = i; j < mp; j++)
                            {
                                mX0[j] = mX0[j + 1];
                                mY0[j] = mY0[j + 1];
                            }
                            mp--;
                            i--;///i减1
                            continue;//再增1
                        }
                        mX0[i] = Math.Log10(mX0[i]);//+0.7;
                    }
                }
            }
            double x, y1, y2, xx;
            int poly_n = 1;
            int pro = mLogFlag ? 1 : 2/*0*/;
            double jjj;
            int ii = 0, jj = 0;
            int iFlag = 0;
            double yyy = 0;

            double tss = 0, ess = 0;
            //下面部分是 2011-12-05 与PC端拟合软件同步更新
            int li_End = 0, li_Start = 0;
            if (iPower == 0)//无限制
            {
                li_Start = 0;
                li_End = iMaxPower;
            }
            else
            {
                li_Start = iPower - 1;
                li_End = iPower;
            }
            //2011-04-13 去掉判断拐点（原来是以上升趋势判定为正常，如果下降了则表示有拐点）

            if (!mSubFlag)	//如果没有分段 开始拟合方程
            {
                //poly_n = 1;
                iFlag = 0;
                poly_n = li_Start + 1;
                //add by zhou zhang kui 2011-06-13
                for (i = li_Start; i < li_End; i++)
                //for(i=0;i<6;i++)
                {
                    if (mp < 3)
                    {
                        i = 6;
                        break;
                    }
                    for (int nn = 0; nn < 16; nn++)
                        a0[nn] = 0;
                    polyfit(mp, mX0, mY0, poly_n, a0);//拟合方程1
                    //检查是否有拐点 modi by zhou zhang kui 2012-02-09
                    bool bl = true;
                    for (x = mX0[0]; x <= mX0[mp - 1]; )
                    {
                        y1 = a0[0] + a0[1] * x + a0[2] * x * x + a0[3] * x * x * x + a0[4] * x * x * x * x + a0[5] * x * x * x * x * x + a0[6] * x * x * x * x * x * x;
                        if (mLogFlag)
                            x = x + 0.01;
                        else
                            x = x + 0.1;
                        //	x = x+0.5;
                        y2 = a0[0] + a0[1] * x + a0[2] * x * x + a0[3] * x * x * x + a0[4] * x * x * x * x + a0[5] * x * x * x * x * x + a0[6] * x * x * x * x * x * x;
                        if (y2 < y1)
                        {
                            bl = false;
                            break;
                        }
                    }
                    if (bl)//upp)
                    {
                        for (jj = 0; jj < poly_n + 1; jj++)
                            y1 = ma0[ii, jj] = a0[jj];
                        ii++;
                    }
                    poly_n += 1;
                }
                if (ii == 0)
                {
                    MessageBox.Show("拟合方程有拐点,没有找到合适方程");
                    for (j = 0; j < 16; j++)
                        a0[j] = 0;
                    return 0;
                }

                j = 0;
                //modi by zhou zhang kui 2011-03-25
                double yy = 0, y = 0, ld_R = 0;
                for (int nn = 0; nn < mp; nn++)
                {
                    yy += mY0[nn];
                }
                yy = yy / mp;		//观测值的平均值
                //求所有曲线中R的值接近1的曲线 
                i = 0;
                for (i = 0; i < ii; i++)
                //for(i=li_Start;i<li_End;i++) 
                {
                    tss = 0;
                    ess = 0;
                    for (int e = 0; e < mp; e++)
                    {
                        xx = mX0[e];
                        tss += (mY0[e] - yy) * (mY0[e] - yy);//总离差平方和
                        //x = log10(mX0[i]);

                        //y = a[0]+a[1]*x+a[2]*x*x;
                        y = ma0[i, 0] + ma0[i, 1] * xx + ma0[i, 2] * xx * xx + ma0[i, 3] * xx * xx * xx
                            + ma0[i, 4] * xx * xx * xx * xx + ma0[i, 5] * xx * xx * xx * xx * xx
                            + ma0[i, 6] * xx * xx * xx * xx * xx * xx;


                        //y = k*x + b ;
                        ess += (y - yy) * (y - yy);//回归平方和
                        //rss += ( mY0[i]-y)*( mY0[i]-y);//残差平方和
                    }
                    ld_R = Math.Sqrt(ess / tss);  //R的值
                    if (Orss < ld_R)
                    {
                        Orss = ld_R;
                        ld_R = 0;
                        iFlag = i;
                        j = i + 1;
                        k = 1;
                    }
                }
                for (i = 0; i <= 6; i++)
                {
                    a0[i] = ma0[iFlag, i];
                    if (a0[i] != 0)
                        j = i;
                }
                n[0] = j;
                Orss = 0;
            }
            else	//有分段的时候
            {
                //第一段
                //poly_n = 1;
                poly_n = li_Start + 1;
                for (i = li_Start; i < li_End; i++)
                //for(i=0;i<6;i++)
                {
                    if (mp < 3)
                    {
                        i = 6;
                        break;
                    }
                    for (j = 0; j < 16; j++)
                        a0[j] = 0;
                    polyfit(mp, mX0, mY0, poly_n, a0);
                    //检查是否有拐点 modi by zhou zhang kui 2012-02-09
                    bool bl = true;
                    for (x = mX0[0]; x <= mX0[mp - 1]; )
                    {
                        y1 = a0[0] + a0[1] * x + a0[2] * x * x + a0[3] * x * x * x + a0[4] * x * x * x * x + a0[5] * x * x * x * x * x + a0[6] * x * x * x * x * x * x;
                        if (mLogFlag)
                            x = x + 0.01;
                        else
                            x = x + 0.1;
                        //	x = x+0.5;
                        y2 = a0[0] + a0[1] * x + a0[2] * x * x + a0[3] * x * x * x + a0[4] * x * x * x * x + a0[5] * x * x * x * x * x + a0[6] * x * x * x * x * x * x;
                        if (y2 < y1)
                        {
                            bl = false;
                            break;
                        }
                    }
                    if (bl)//upp)
                    {
                        for (jj = 0; jj < poly_n + 1; jj++)
                            ma0[ii, jj] = a0[jj];
                        ii++;
                    }
                   
                    double myy;
                    myy = a0[6];
                    poly_n += 1;
                }//end of for(i=0;i<7;i++)
                k = 1;
                if (ii == 0)
                {
                    MessageBox.Show("出现拐点,没有找到第一段的拟合方程!");
                    for (j = 0; j < 16; j++)
                        a0[j] = 0;
                    k = 0;
                    poly_n = 0;
                }
                else
                {
                    j = 0;
                    iFlag = 0;

                    //modi by zhou zhang kui 2011-03-25
                    double yy = 0, y = 0, ld_R = 0;
                    for (i = 0; i < mp; i++)
                    {
                        yy += mY0[i];
                    }
                    yy = yy / mp;		//观测值的平均值
                    //求所有曲线中R的值接近1的曲线 
                    for (i = 0; i < ii; i++)
                    // for(i=li_Start;i<li_End;i++) 
                    {
                        tss = 0;
                        ess = 0;
                        for (int e = 0; e < mp; e++)
                        {
                            xx = mX0[e];
                            tss += (mY0[e] - yy) * (mY0[e] - yy);//总离差平方和
                            //x = log10(mX0[i]);

                            //y = a[0]+a[1]*x+a[2]*x*x;
                            y = ma0[i, 0] + ma0[i, 1] * xx + ma0[i, 2] * xx * xx + ma0[i, 3] * xx * xx * xx
                                + ma0[i, 4] * xx * xx * xx * xx + ma0[i, 5] * xx * xx * xx * xx * xx
                                + ma0[i, 6] * xx * xx * xx * xx * xx * xx;


                            //y = k*x + b ;
                            ess += (y - yy) * (y - yy);//回归平方和
                            //rss += ( mY0[i]-y)*( mY0[i]-y);//残差平方和
                        }
                        ld_R = Math.Sqrt(ess / tss);  //R的值
                        if (Orss < ld_R)
                        {
                            Orss = ld_R;
                            ld_R = 0;
                            iFlag = i;
                            j = i + 1;
                            k = 1;
                        }
                    }
                   
                    for (i = 0; i < 16; i++)
                        a0[i] = 0;
                    for (i = 0; i <= 6; i++)
                    {
                        a0[i] = ma0[iFlag, i];
                        if (a0[i] != 0)
                            j = i;
                    }
                    yyy = a0[6];
                    n[0] = j;
                }
                ii = 0;
                Orss = 10;
                //第二段

                if (iPower2 == 0)
                {
                    li_Start = 0;
                    li_End = iMaxPower;
                }
                else
                {
                    li_Start = iPower2 - 1;
                    li_End = iPower2;
                }
                //poly_n = 1;
                poly_n = li_Start + 1;
                for (i = li_Start; i < li_End; i++)
                //for(i=0;i<6;i++)
                {
                    if (mq < 3)
                    {
                        i = 6;
                        break;
                    }
                    for (j = 0; j < 16; j++)
                        a1[j] = 0;
                    polyfit(mq, mX1, mY1, poly_n, a1);
                    //检查是否有拐点 modi by zhou zhang kui 2012-02-09
                    bool bl = true;
                    for (x = mX1[0]; x <= mX1[mq - 1]; )
                    {
                        y1 = a1[0] + a1[1] * x + a1[2] * x * x + a1[3] * x * x * x + a1[4] * x * x * x * x
                            + a1[5] * x * x * x * x * x + a1[6] * x * x * x * x * x * x;
                        if (mLogFlag)
                            x += 0.01;
                        else
                            x += 0.1;
                        y2 = a1[0] + a1[1] * x + a1[2] * x * x + a1[3] * x * x * x + a1[4] * x * x * x * x
                            + a1[5] * x * x * x * x * x + a1[6] * x * x * x * x * x * x;
                        if (y2 < y1)
                        {
                            bl = false;
                            break;
                        }
                    }
                    if (bl)//upp)
                    {
                        for (jj = 0; jj < poly_n + 1; jj++)
                            ma1[ii, jj] = a1[jj];
                        ii++;
                    }
                    
                    poly_n += 1;
                }
                if (ii == 0)
                {
                    MessageBox.Show("出现拐点没有找到第二段的拟合方程!");
                    //MessageBox("拟合方程有拐点,没有找到合适方程");
                    for (j = 0; j < 16; j++)
                        a1[j] = 0;
                    n[1] = 0;
                    return k;
                }
                else
                {
                    j = 0;
                    iFlag = 0;
                    
                    for (i = 0; i < ii; i++)
                    // for(i=li_Start;i<li_End;i++) 
                    {
                        rss = 0;
                        for (int e = 0; e < mq; e++)
                        {
                            xx = mX1[e];
                            y1 = ma1[i, 0] + ma1[i, 1] * xx + ma1[i, 2] * xx * xx + ma1[i, 3] * xx * xx * xx
                                + ma1[i, 4] * xx * xx * xx * xx + ma1[i, 5] * xx * xx * xx * xx * xx
                                + ma1[i, 6] * xx * xx * xx * xx * xx * xx;
                            jjj = Math.Abs(mY1[e] - y1);
                            rss += jjj * jjj;
                        }
                        if (Orss >= rss)
                        {
                            Orss = rss;
                            iFlag = i;
                            j = i + 1;
                        }
                    }


                    for (i = 0; i < 16; i++)
                        a1[i] = 0;
                    for (i = 0; i <= 6; i++)
                    {
                        a1[i] = ma1[iFlag, i];
                        if (a1[i] != 0)
                            j = i;
                    }
                    if (j != 0)
                        k++;
                    n[1] = j;
                    Orss = 10;
                }
            }
            return k;
        }

	 /* 函数功能：最小二乘法曲线拟合
	 * @param n
	 *            变量。给定数据点的个数
	 * @param x
	 *            实型一维数组，长度为 n 。存放给定 n 个数据点的　X　坐标
	 * @param y
	 *            实型一维数组，长度为 n 。存放给定 n 个数据点的　Y　坐标
	 * @param poly_n
	 *            拟合多项式的项数，即拟合多项式的最高次数为 m-1. 要求 m<=n 且m<=20。若 m>n 或 m>20
	 *            ，则本函数自动按 m=min{n,20} 处理.
	 * @param a
	 *            实型一维数组，长度为 m 。返回 m-1　次拟合多项式的 m 个系数
	 * @return d 拟合y=a0+a1*x+a2*x^2+……+apoly_n*x^poly_n 多项式系数存储数组
	 */
        private static void polyfit(int n, double[] x, double[] y, int poly_n, double[] a)
        {
            int i, j;
            //a = new double[16];
            double[] tempx, tempy, sumxx, sumxy, ata;
            tempx = new double[n];
            sumxx = new double[poly_n * 2 + 1];
            tempy = new double[n];

            sumxy = new double[poly_n + 1];
            ata = new double[(poly_n + 1) * (poly_n + 1)];
            for (i = 0; i < n; i++)
            {
                tempx[i] = 1;
                tempy[i] = y[i];
            }
            for (i = 0; i < 2 * poly_n + 1; i++)
                for (sumxx[i] = 0, j = 0; j < n; j++)
                {
                    sumxx[i] += tempx[j];
                    tempx[j] *= x[j];
                }
            for (i = 0; i < poly_n + 1; i++)
                for (sumxy[i] = 0, j = 0; j < n; j++)
                {
                    sumxy[i] += tempy[j];
                    tempy[j] *= x[j];
                }
            for (i = 0; i < poly_n + 1; i++)
                for (j = 0; j < poly_n + 1; j++)
                    ata[i * (poly_n + 1) + j] = sumxx[i + j];
            gauss_solve(poly_n + 1, ata, a, sumxy);

        }

	// [start] gauss_solve
        private static void gauss_solve(int n, double[] A, double[] x, double[] b)
        {
            int i, j, k, r;
            double max;
            for (k = 0; k < n - 1; k++)
            {
                max = Math.Abs(A[k * n + k]); // find maxmum/
                r = k;
                for (i = k + 1; i < n - 1; i++)
                    if (max < Math.Abs(A[i * n + i]))
                    {
                        max = Math.Abs(A[i * n + i]);
                        r = i;
                    }
                if (r != k)
                    for (i = 0; i < n; i++) // change array:A[k]&A[r] /
                    {
                        max = A[k * n + i];
                        A[k * n + i] = A[r * n + i];
                        A[r * n + i] = max;
                    }
                max = b[k]; // change array:b[k]&b[r] /
                b[k] = b[r];
                b[r] = max;
                for (i = k + 1; i < n; i++)
                {
                    for (j = k + 1; j < n; j++)
                        A[i * n + j] -= A[i * n + k] * A[k * n + j] / A[k * n + k];
                    b[i] -= A[i * n + k] * b[k] / A[k * n + k];
                }
            }
            for (i = n - 1; i >= 0; x[i] /= A[i * n + i], i--)
                for (j = i + 1, x[i] = b[i]; j < n; j++)
                    x[i] -= A[i * n + j] * x[j];
        }

    }
}
