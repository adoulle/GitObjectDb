using LibGit2Sharp;
using System;
using System.Collections.Generic;

namespace GitObjectDb.Internal.Commands;

internal interface ICommitCommand
{
    Commit Commit(TransformationComposer composer,
                  CommitDescription description,
                  Action<ITransformation>? beforeProcessing = null);

    Commit Commit(IConnection connection,
                  string branchName,
                  IEnumerable<Delegate> transformations,
                  CommitDescription description,
                  Commit predecessor,
                  bool updateBranchTip = true);
}
