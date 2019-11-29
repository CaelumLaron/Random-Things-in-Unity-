#include <bits/stdc++.h>
using namespace std;

using ll = long long;
const int maxn = 100010;
int n;
int tree[1000][maxn],v[maxn], pai[maxn];
//int tree[maxn];

int find(int x){
	if(pai[x] == x)
		return x;
	return pai[x] = find(pai[x]);
}

void join(int a, int b){
	a = find(a); b = find(b);
	if(a != b)
		pai[b] = a;
}

void update(int idx, int value, int comp){
	for(; idx <= n; idx+=(idx & -idx))
		tree[comp][idx] += value;
}

int get_sum(int idx, int comp){
	int sum = 0;
	for(; idx > 0; idx-=(idx & -idx))
		sum += tree[comp][idx];
	return sum;
}

int main(){
	int x;
	scanf("%d", &x);
	for(int i=0; i<x; i++){
		pai[i] = i;
		scanf("%d", v+i);
	}
	int m;
	scanf("%d", &m);
	for(int i=0; i<m; i++){
		int a, b; scanf("%d%d", &a, &b);
		join(a-1, b-1);
	}
	int c = 0;
	map<int, int> mp;
	vector<vector<int>> adj;
	for(int i=0; i<x; i++){
		if(mp.count(find(i)) == 0){
			mp[find(i)] = c++;
			adj.push_back(vector<int>());
		}
		adj[mp[find(i)]].push_back(i);
	}
	for(auto i : adj){
		for(auto j : i){
			printf("%d ", j);
		}
		printf("\n");
	}
	return 0;
}

//1088 contagem de inverssões OK
//1112 Fenwick 2d
//1804 Fenwick normal OK
//2537 Fenwick 2d
//2538 Fenwick normal OK
//2539 contagem de inversões OK
//2945 Fenwick normal
/*
int get_sum(int idx, int idy){// y > x
	int x = get_sum(idx-1), y = get_sum(idy);
	return y - x;
}

ll get_inv(){
	int _max = -1;
	for(int i=0; i<n; i++)
		_max = max(_max, v[i]);
	for(int i=1; i<=n; i++)
		tree[i] = 0;
	ll inv = 0;
	//printf("%d\n", n);
	for(int i=n-1; i>=0; i--){
		inv += get_sum(v[i]);
		update(v[i], 1, _max);
	}
	return inv;
}

void update(int idx, int value, int limit){
	for(; idx <= limit; idx+=(idx & -idx))
		tree[idx] += value;
}
*/
