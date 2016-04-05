﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using SolidworksAddinFramework.OpenGl;
using SolidWorks.Interop.sldworks;
using Weingartner.Numerics;
using Xunit;
using Xunit.Extensions;
using XUnit.Solidworks.Addin;
using XUnitRemote;

namespace SolidworksAddinFramework.Spec
{
    public class MathUtilitySpec : SolidWorksSpec
    {
        public IMathUtility MathUtility =>  (IMathUtility) SwApp.GetMathUtility();

        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] {new[] {0,0,0.0}, new[] {1,0,0.0}, 1e-3, 1001};
                yield return new object[] {new[] {0,0,0.0}, new[] {1,1,1.0}, 1e-3, 1734};
            }
        }


        [SolidworksFact]
        public void ShouldBeAbleToGetCrossProduct()
        {
            1.Should().Be(2);
            var result = MathUtility.Cross(new [] { 1.0, 2.0, 3.0 }, new [] { 4.0, 5.0, 6.0 });
            result.ShouldAllBeEquivalentTo(new[] { -3.0, 6.0, -3.0 });
        }


        [SolidworksFact]
        public void RotateByAngleShouldWork()
        {
            var p = MathUtility.Point(new[] { 1, 0.0, 0 });
            var zAxis = MathUtility.Vector(new[] { 0, 0, 1.0 });

            Sequences.LinSpace(0, 2 * Math.PI, 10, false)
                .ToList()
                .ForEach(a =>
                {
                    var newPoint = MathUtility.RotateByAngle(p, zAxis, a);
                    newPoint.Angle2D().Should().BeApproximately(a, 1e-5);
                });
        }

        /*
        [Theory, MemberData(nameof(TestData))]
        public void InterpolatePointsShouldWork(double[]p0,double[]p1,double stepSize, int expectedPointCount)
        {
            var points = new[] {p0, p1};

            var interpolatedPoints = MathUtility.InterpolatePoints(points, stepSize);
            interpolatedPoints.Count.Should().Be(expectedPointCount);
            interpolatedPoints
                .Buffer(2, 1)
                .Where(p => p.Count == 2)
                .ForEach(p =>
                {
                    MathUtility.Vector(p[0].ToArray(), p[1].ToArray()).GetLength().Should().BeApproximately(stepSize,1e-5);
                });
        }
        */
    }
}
