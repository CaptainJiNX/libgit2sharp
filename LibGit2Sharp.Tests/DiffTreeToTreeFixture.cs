                var commit = repo.Lookup("32eab9cb1f450b5fe7ab663462b77d7f4b703344", GitObjectType.Commit) as Commit;
                Tree commitTree = commit.Tree;
                Tree parentCommitTree = commit.Parents.Single().Tree;
        /*
         * $ git diff 9fd738e..HEAD -- "1" "2/"
         * diff --git a/1/branch_file.txt b/1/branch_file.txt
         * new file mode 100755
         * index 0000000..45b983b
         * --- /dev/null
         * +++ b/1/branch_file.txt
         * @@ -0,0 +1 @@
         * +hi
         */
        [Fact]
        public void CanCompareASubsetofTheTreeAgainstOneOfItsAncestor()
        {
            using (var repo = new Repository(StandardTestRepoPath))
            {
                Tree tree = repo.Head.Tip.Tree;
                Tree ancestor = repo.Lookup<Commit>("9fd738e").Tree;

                TreeChanges changes = repo.Diff.Compare(ancestor, tree, new[]{ "1", "2/" });
                Assert.NotNull(changes);

                Assert.Equal(1, changes.Count());
                Assert.Equal("1/branch_file.txt", changes.Added.Single().Path);
            }
        }
