//*****************************************************************************
//** 959. Regions Cut By Slashes  leetcode                                   **
//*****************************************************************************
//*****************************************************************************


int find(int parent[], int x)
{
    if (parent[x] != x)
    {
        parent[x] = find(parent, parent[x]);
    }
    return parent[x];
}

void unionSet(int parent[], int rank[], int x, int y)
{
    int rootX = find(parent, x);
    int rootY = find(parent, y);
    if (rootX != rootY)
    {
        if (rank[rootX] > rank[rootY])
        {
            parent[rootY] = rootX;
        }
        else if (rank[rootX] < rank[rootY])
        {
            parent[rootX] = rootY;
        }
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
    }
}

int regionsBySlashes(char **grid, int gridSize)
{
    int n = gridSize;
    int TRIANGLE_COUNT = 4 * n * n;  // Correctly calculate TRIANGLE_COUNT
    int parent[TRIANGLE_COUNT];
    int rank[TRIANGLE_COUNT];

    for (int i = 0; i < TRIANGLE_COUNT; i++)
    {
        parent[i] = i;
        rank[i] = 0;
    }

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int base = (i * n + j) * 4;
            char c = grid[i][j];

            if (c == '/')
            {
                unionSet(parent, rank, base + 0, base + 3);
                unionSet(parent, rank, base + 1, base + 2);
            }
            else if (c == '\\')
            {
                unionSet(parent, rank, base + 0, base + 1);
                unionSet(parent, rank, base + 2, base + 3);
            }
            else
            {
                unionSet(parent, rank, base + 0, base + 1);
                unionSet(parent, rank, base + 1, base + 2);
                unionSet(parent, rank, base + 2, base + 3);
            }

            // Connect with the cell below
            if (i < n - 1)
            {
                unionSet(parent, rank, base + 2, base + 4 * n + 0);
            }

            // Connect with the cell to the right
            if (j < n - 1)
            {
                unionSet(parent, rank, base + 1, base + 4 + 3);
            }
        }
    }

    int regions = 0;
    for (int i = 0; i < TRIANGLE_COUNT; i++)
    {
        if (find(parent, i) == i)
        {
            regions++;
        }
    }

    return regions;
}