﻿using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace ReactGraph.Tests
{
    public class ExpressionParserTest
    {
        [Fact]
        public void SimpleExpressionGetsAddedToGraph()
        {
            var graph = new DirectedGraph<NodeInfo>();
            var expressionParser = new ExpressionParser();

            var basicType = new BasicType
            {
                Source1 = 1,
                Source2 = 2
            };
            expressionParser.AddToGraph(graph, () => basicType.Target, () => TargetFormula(basicType.Source1, basicType.Source2));

            graph.EdgesCount.ShouldBe(2);
            graph.VerticiesCount.ShouldBe(3);
            graph.Verticies.ShouldContain(v => v.Data.PropertyInfo.Name == "Target" && v.Data.Instance == basicType);
            graph.Verticies.ShouldContain(v => v.Data.PropertyInfo.Name == "Source1" && v.Data.Instance == basicType);
            graph.Verticies.ShouldContain(v => v.Data.PropertyInfo.Name == "Source2" && v.Data.Instance == basicType);
            var targetVertex = graph.Verticies.Single(v => v.Data.PropertyInfo.Name == "Target" && v.Data.Instance == basicType);
            targetVertex.Data.ReevalValue();
            basicType.Target.ShouldBe("3");
            targetVertex.Predecessors.Count().ShouldBe(2);
            targetVertex.Successors.Count().ShouldBe(0);
        }

        private string TargetFormula(int source1, int source2)
        {
            return (source1 + source2).ToString();
        }
    }
}
