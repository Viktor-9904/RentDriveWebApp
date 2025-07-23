import { useEffect, useState } from "react";

export function useOverview() {
  const [overviewData, setOverviewData] = useState(null);
  const [overviewLoading, setOverviewLoading] = useState(true);
  const [overviewError, setOverviewError] = useState(null);
  const backEndURL = import.meta.env.VITE_API_URL;

  useEffect(() => {
    const fetchOverview = async () => {
      try {
        const response = await fetch(`${backEndURL}/api/account/overview-details`, {
                method: "GET",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
            })
        console.log(response)

        if (!response.ok){
            throw new Error("Failed to fetch overview details");
        }

        const result = await response.json();
        console.log(result)
        setOverviewData(result);

      } catch (err) {
        setOverviewError(err.message);
      } finally {
        setOverviewLoading(false);
      }
    };

    fetchOverview();
  }, []);

  return { overviewData, overviewLoading, overviewError };
}
