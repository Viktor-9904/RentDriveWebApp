import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export function useOverview() {
    const [overviewData, setOverviewData] = useState(null);
    const [overviewLoading, setOverviewLoading] = useState(true);
    const [overviewError, setOverviewError] = useState(null);
    const backEndURL = useBackendURL();

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
                if (!response.ok) {
                    throw new Error("Failed to fetch overview details");
                }

                const result = await response.json();
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
