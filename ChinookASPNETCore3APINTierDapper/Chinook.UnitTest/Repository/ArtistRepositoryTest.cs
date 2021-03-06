﻿using System;
using Chinook.MockData.Repositories;
using Chinook.Domain.Entities;
using JetBrains.dotMemoryUnit;
using Xunit;

[assembly: SuppressXUnitOutputException]
[assembly: EnableDotMemoryUnitSupport]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Chinook.UnitTest.Repository
{
    public class ArtistRepositoryTest
    {
        private readonly ArtistRepository _repo;

        public ArtistRepositoryTest()
        {
            _repo = new ArtistRepository();
        }

        [DotMemoryUnitAttribute(FailIfRunWithoutSupport = false)]
        [Fact]
        public void ArtistGetAll()
        {
            // Act
            var artists = _repo.GetAll();

            // Assert
            Assert.Single(artists);
        }

        [AssertTraffic(AllocatedSizeInBytes = 1000, Types = new[] {typeof(Artist)})]
        [Fact]
        public void DotMemoryUnitTest()
        {
            var repo = new ArtistRepository();

            repo.GetAll();

            dotMemory.Check(memory =>
                Assert.Equal(1, memory.GetObjects(where => where.Type.Is<Artist>()).ObjectsCount));

            GC.KeepAlive(repo); // prevent objects from GC if this is implied by test logic
        }
    }
}